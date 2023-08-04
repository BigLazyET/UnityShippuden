using Assets.Scripts.Combat;
using Assets.Scripts.ModifierSystem;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        // set in inspector
        [SerializeField] private GameObject damageParticles;

        public BodyStatus BodyStatus => BodyStatus ?? core.GetCoreComponent<BodyStatus>();

        public ParticleManager ParticleManager => ParticleManager ?? core.GetCoreComponent<ParticleManager>();

        public ModifyManager<Modifier<DamageData>, DamageData> DamageModifyManager => new();

        public void Damage(DamageData damageData)
        {
            print($"Damage Amount Before Modifiers: {damageData.Amount}");
            damageData = DamageModifyManager.ApplyAllModifiers(damageData);
            print($"Damage Amount After Modifiers: {damageData.Amount}");

            BodyStatus.Health.Decrease(damageData.Amount);
            ParticleManager.StartParticlesWithRandomQuaternion(damageParticles);
        }
    }
}
