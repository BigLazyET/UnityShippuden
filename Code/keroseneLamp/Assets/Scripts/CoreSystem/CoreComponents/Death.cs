using UnityEngine;

namespace Assets.Scripts.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;

        public DamageReceiver DamageReceiver => DamageReceiver ?? core.GetCoreComponent<DamageReceiver>();

        public ParticleManager ParticleManager => ParticleManager ?? core.GetCoreComponent<ParticleManager>();

        private void OnEnable()
        {
            DamageReceiver.Health.OnCurrentValueZero += HandleCurrentValueZero;
        }

        private void OnDisable()
        {
            DamageReceiver.Health.OnCurrentValueZero -= HandleCurrentValueZero;
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
