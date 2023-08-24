using UnityEngine;

namespace Assets.Scripts.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;

        public BodyStatus BodyStatus => BodyStatus ?? core.GetCoreComponent<BodyStatus>();

        public ParticleManager ParticleManager => ParticleManager ?? core.GetCoreComponent<ParticleManager>();

        private void OnEnable()
        {
            BodyStatus.Health.OnCurrentValueZero += HandleCurrentValueZero;
        }

        private void OnDisable()
        {
            BodyStatus.Health.OnCurrentValueZero -= HandleCurrentValueZero;
        }

        private void HandleCurrentValueZero()
        {
            foreach (var particle in deathParticles)
            {
                ParticleManager.StartParticles(particle);
            }

            core.Root.SetActive(false);
        }
    }
}
