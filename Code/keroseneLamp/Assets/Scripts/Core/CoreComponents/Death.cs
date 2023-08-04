using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;

        public BodyStatus BodyStatus => BodyStatus ?? core.GetCoreComponent<BodyStatus>();

        public ParticleManager ParticleManager => ParticleManager ?? core.GetCoreComponent<ParticleManager>();

        private void OnEnable()
        {
            BodyStatus.Health.OnCurrentValueZero += Health_OnCurrentValueZero;
        }

        private void OnDisable()
        {
            BodyStatus.Health.OnCurrentValueZero -= Health_OnCurrentValueZero;
        }

        private void Health_OnCurrentValueZero()
        {
            foreach (var particle in deathParticles)
            {
                ParticleManager.StartParticles(particle);
            }

            core.transform.parent.gameObject.SetActive(false);
        }
    }
}
