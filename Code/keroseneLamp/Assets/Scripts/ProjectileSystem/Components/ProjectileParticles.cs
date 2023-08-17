using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    public class ProjectileParticles : ProjectileComponent
    {
        [field: SerializeField] public ParticleSystem ParticleSystem { get; private set; }

        public void SpawnImpactParticles(Vector3 position, Quaternion rotation)
        {
            Instantiate(ParticleSystem, position, rotation);
        }

        public void SpawnImpactParticles(RaycastHit2D hit)
        {
            var rotation = Quaternion.FromToRotation(transform.right, hit.normal);

            SpawnImpactParticles(hit.point, rotation);
        }
    }
}
