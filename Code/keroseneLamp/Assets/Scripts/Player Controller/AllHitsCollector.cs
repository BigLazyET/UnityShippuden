using Unity.Assertions;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Physics;

namespace PlayerController
{
    public struct AllHitsCollector<T> : ICollector<T> where T : unmanaged, IQueryResult
    {
        private int m_selfRgidBodyIndex;
        private PhysicsWorld m_world;

        public bool EarlyOutOnFirstHit => false;

        public float MaxFraction { get; }

        public int NumHits => AllHits.Length;

        public float MinHitFraction;

        public NativeList<T> AllHits;

        public NativeList<T> TriggerHits;

        public AllHitsCollector(int rbIndex, float maxFraction, ref NativeList<T> allHits,
                                    PhysicsWorldSingleton physicsWorldSingleton,
                                    NativeList<T> triggerHits = default)
        {
            MaxFraction = maxFraction;
            AllHits = allHits;
            m_selfRgidBodyIndex = rbIndex;
            m_world = physicsWorldSingleton.PhysicsWorld;
            TriggerHits = triggerHits;
            MinHitFraction = float.MaxValue;
        }

        public AllHitsCollector(int rbIndex, float maxFraction, ref NativeList<T> allHits, PhysicsWorld world,
                                NativeList<T> triggerHits = default)
        {
            MaxFraction = maxFraction;
            AllHits = allHits;
            m_selfRgidBodyIndex = rbIndex;
            m_world = world;
            TriggerHits = triggerHits;
            MinHitFraction = float.MaxValue;
        }

        public bool AddHit(T hit)
        {
            Assert.IsTrue(hit.Fraction < MaxFraction);

            if (hit.RigidBodyIndex == m_selfRgidBodyIndex) return false;

            if(hit.Material.CollisionResponse == CollisionResponsePolicy.RaiseTriggerEvents)
            {
                if(TriggerHits.IsCreated)
                    TriggerHits.Add(hit);
                return false;
            }

            MinHitFraction = math.min(MinHitFraction, hit.Fraction);
            AllHits.Add(hit);
            return true;
        }
    }
}
