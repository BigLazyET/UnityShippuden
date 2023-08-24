using UnityEngine;

namespace Assets.Scripts.CoreSystem
{
    public class ParticleManager : CoreComponent
    {
        private Movement movement;
        private Transform particleContainer;

        protected override void Awake()
        {
            base.Awake();

            particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
        }

        private void Start()
        {
            movement = core.GetCoreComponent<Movement>();
        }

        public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion quaternion)
        {
            return Instantiate(particlePrefab, position, quaternion, particleContainer);
        }

        public GameObject StartParticles(GameObject particlePrefab) => StartParticles(particlePrefab, transform.position, Quaternion.identity);

        public GameObject StartParticlesWithRandomQuaternion(GameObject particlePrefab)
        {
            var quaternion = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            return StartParticles(particlePrefab, transform.position, quaternion);
        }

        // Spawns particles relative to transform based on offset (input parameter) and FacingDirection
        public GameObject StartParticlesRelative(GameObject particlePrefab, Vector2 offset, Quaternion quaternion)
        {
            offset.x *= movement.FacingDirection;
            var spawnPos = transform.position + (Vector3)offset;
            return StartParticles(particlePrefab, spawnPos, quaternion);
        }
    }
}
