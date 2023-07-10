using Events;
using Unity.Assertions;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;

namespace PlayerController
{
    public class Util
    {
        const float k_SimplexSolverEpsilon = 0.0001f;
        const float k_SimplexSolverEpsilonSq = k_SimplexSolverEpsilon * k_SimplexSolverEpsilon;

        const int k_DefaultQueryHitsCapacity = 8;
        const int k_DefaultConstraintsCapacity = 2 * k_DefaultQueryHitsCapacity;

        public static void CheckSupport(
            in PhysicsWorldSingleton physicsWorldSingleton, ref PhysicsCollider collider, StepInput stepInput,
            RigidTransform transform,
            out PlayerSupportState characterState, out float3 surfaceNormal, out float3 surfaceVelocity,
            NativeList<StatefulCollisionEvent> collisionEvents = default)
        {
            surfaceNormal = float3.zero; surfaceVelocity = float3.zero;

            // Up direction must be normalized
            Assert.IsTrue(Math.IsNormalized(stepInput.Up));

            // Query the world
            NativeList<ColliderCastHit> castHits = new NativeList<ColliderCastHit>(k_DefaultQueryHitsCapacity, Allocator.Temp);
            AllHitsCollector<ColliderCastHit> castHitsCollector = new AllHitsCollector<ColliderCastHit>(
                stepInput.RigidBodyIndex, 1.0f, ref castHits, physicsWorldSingleton);

            var maxDisplacement = -stepInput.ContactTolerance * stepInput.Up;
            {
                var input = new ColliderCastInput(collider.Value, transform.pos, transform.pos + maxDisplacement, transform.rot);
                physicsWorldSingleton.PhysicsWorld.CastCollider(input, ref castHitsCollector);
            }

            // If no hits
            if (castHitsCollector.NumHits == 0)
            {
                characterState = PlayerSupportState.UnSupported;
                return;
            }

            var maxSlopeCos = math.cos(stepInput.MaxSlope);

            // Iterate over distance hits and create constraints from them
            NativeList<SurfaceConstraintInfo> constraints = new NativeList<SurfaceConstraintInfo>(k_DefaultConstraintsCapacity, Allocator.Temp);
            var maxDisplacementLength = math.length(maxDisplacement);
            for (int i = 0; i < castHitsCollector.NumHits; i++)
            {
                var hit = castHitsCollector.AllHits[i];
                CreateConstraint(stepInput.PhysicsWorldSingleton.PhysicsWorld, stepInput.Up,
                    hit.RigidBodyIndex, hit.ColliderKey, hit.Position, hit.SurfaceNormal,
                    hit.Fraction * maxDisplacementLength,
                    stepInput.SkinWidth, maxSlopeCos, ref constraints);
            }

            // Velocity for support checking
            var initialVelocity = maxDisplacement / stepInput.DeltaTime;
            Math.ClampToMaxLength(stepInput.MaxMovementSpeed, ref initialVelocity);

            // Solve downwards (don't use min delta time, try to solve full step)
            float3 outVelocity = initialVelocity;
            float3 outPosition = transform.pos;
            SimplexSolver.Solve(stepInput.DeltaTime, stepInput.DeltaTime, stepInput.Up, stepInput.MaxMovementSpeed,
                constraints, ref outPosition, ref outVelocity, out float integratedTime, false);

            // Get info on surface
            int numSupportingPlanes = 0;
            {
                for (int j = 0; j < constraints.Length; j++)
                {
                    var constraint = constraints[j];
                    if (constraint.Touched && !constraint.IsTooSteep && !constraint.IsMaxSlope)
                    {
                        numSupportingPlanes++;
                        surfaceNormal += constraint.Plane.Normal;
                        surfaceVelocity += constraint.Velocity;

                        // Add supporting planes to collision events
                        if (collisionEvents.IsCreated)
                        {
                            CollisionWorld world = stepInput.PhysicsWorldSingleton.PhysicsWorld.CollisionWorld;
                            var collisionEvent = new StatefulCollisionEvent()
                            {
                                EntityA = world.Bodies[stepInput.RigidBodyIndex].Entity,
                                EntityB = world.Bodies[constraint.RigidBodyIndex].Entity,
                                BodyIndexA = stepInput.RigidBodyIndex,
                                BodyIndexB = constraint.RigidBodyIndex,
                                ColliderKeyA = ColliderKey.Empty,
                                ColliderKeyB = constraint.ColliderKey,
                                Normal = constraint.Plane.Normal
                            };
                            collisionEvent.CollisionDetails =
                                new Details(1, 0, constraint.HitPosition);
                            collisionEvents.Add(collisionEvent);
                        }
                    }
                }

                if (numSupportingPlanes > 0)
                {
                    float invNumSupportingPlanes = 1.0f / numSupportingPlanes;
                    surfaceNormal *= invNumSupportingPlanes;
                    surfaceVelocity *= invNumSupportingPlanes;

                    surfaceNormal = math.normalize(surfaceNormal);
                }
            }
            // Check support state
            {
                if (math.lengthsq(initialVelocity - outVelocity) < k_SimplexSolverEpsilonSq)
                {
                    // If velocity hasn't changed significantly, declare unsupported state
                    characterState = PlayerSupportState.UnSupported;
                }
                else if (math.lengthsq(outVelocity) < k_SimplexSolverEpsilonSq && numSupportingPlanes > 0)
                {
                    // If velocity is very small, declare supported state
                    characterState = PlayerSupportState.Supported;
                }
                else
                {
                    // Check if sliding
                    outVelocity = math.normalize(outVelocity);
                    float slopeAngleSin = math.max(0.0f, math.dot(outVelocity, -stepInput.Up));
                    float slopeAngleCosSq = 1 - slopeAngleSin * slopeAngleSin;
                    if (slopeAngleCosSq <= maxSlopeCos * maxSlopeCos)
                    {
                        characterState = PlayerSupportState.Sliding;
                    }
                    else if (numSupportingPlanes > 0)
                    {
                        characterState = PlayerSupportState.Supported;
                    }
                    else
                    {
                        // If numSupportingPlanes is 0, surface normal is invalid, so state is unsupported
                        characterState = PlayerSupportState.UnSupported;
                    }
                }
            }
        }

        private static void CreateConstraint(PhysicsWorld world, float3 up,
            int hitRigidBodyIndex, ColliderKey hitColliderKey, float3 hitPosition, float3 hitSurfaceNormal,
            float hitDistance,
            float skinWidth, float maxSlopeCos, ref NativeList<SurfaceConstraintInfo> constraints)
        {
            CreateConstraintFromHit(world, hitRigidBodyIndex, hitColliderKey, hitPosition, hitSurfaceNormal, hitDistance, skinWidth, out SurfaceConstraintInfo constraint);

            // Check if max slope plane is required
            var verticalComponent = math.dot(constraint.Plane.Normal, up);
            var shouldAddPlane = verticalComponent > k_SimplexSolverEpsilon && verticalComponent < maxSlopeCos;
            if (shouldAddPlane)
            {
                constraint.IsTooSteep = true;
                CreateMaxSlopeConstraint(up, ref constraint, out SurfaceConstraintInfo maxSlopeConstraint);
                constraints.Add(maxSlopeConstraint);
            }

            // Prepare velocity to resolve penetration
            ResolveConstraintPenetration(ref constraint);

            // Add original constraint to the list
            constraints.Add(constraint);
        }

        private static void CreateConstraintFromHit(PhysicsWorld world, int rigidBodyIndex, ColliderKey colliderKey,
            float3 hitPosition, float3 normal, float distance, float skinWidth, out SurfaceConstraintInfo constraint)
        {
            // TODO：RigidBody有三种类型：https://zhuanlan.zhihu.com/p/552012383
            bool bodyIsDynamic = 0 <= rigidBodyIndex && rigidBodyIndex < world.NumDynamicBodies;
            constraint = new SurfaceConstraintInfo()
            {
                Plane = new Plane
                {
                    Normal = normal,
                    Distance = distance - skinWidth,
                },
                RigidBodyIndex = rigidBodyIndex,
                ColliderKey = colliderKey,
                HitPosition = hitPosition,
                Velocity = bodyIsDynamic ? world.GetLinearVelocity(rigidBodyIndex, hitPosition) : float3.zero,
                Priority = bodyIsDynamic ? 1 : 0
            };
        }

        private static void ResolveConstraintPenetration(ref SurfaceConstraintInfo constraint)
        {
            // Fix up the velocity to enable penetration recovery
            if (constraint.Plane.Distance < 0.0f)
            {
                float3 newVel = constraint.Velocity - constraint.Plane.Normal * constraint.Plane.Distance;
                constraint.Velocity = newVel;
                constraint.Plane.Distance = 0.0f;
            }
        }

        private static void CreateMaxSlopeConstraint(float3 up, ref SurfaceConstraintInfo constraint, out SurfaceConstraintInfo maxSlopeConstraint)
        {
            var verticalComponent = math.dot(constraint.Plane.Normal, up);

            var newConstraint = constraint;
            newConstraint.Plane.Normal = math.normalize(newConstraint.Plane.Normal - verticalComponent * up);
            newConstraint.IsMaxSlope = true;

            float distance = newConstraint.Plane.Distance;

            // Calculate distance to the original plane along the new normal.
            // Clamp the new distance to 2x the old distance to avoid penetration recovery explosions.
            newConstraint.Plane.Distance =
                distance / math.max(math.dot(newConstraint.Plane.Normal, constraint.Plane.Normal), 0.5f);

            if (newConstraint.Plane.Distance < 0.0f)
            {
                // Disable penetration recovery for the original plane
                constraint.Plane.Distance = 0.0f;

                // Prepare velocity to resolve penetration
                ResolveConstraintPenetration(ref newConstraint);
            }

            // Output max slope constraint
            maxSlopeConstraint = newConstraint;
        }
    }
}
