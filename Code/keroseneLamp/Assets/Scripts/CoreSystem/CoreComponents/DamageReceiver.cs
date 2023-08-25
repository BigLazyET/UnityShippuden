using Assets.Scripts.Combat;
using Assets.Scripts.ModifierSystem;
using UnityEngine;

namespace Assets.Scripts.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [field: SerializeField] public BodyStatu Health { get; private set; }  // 理解：玩家的生命初始值

        [SerializeField] private GameObject damageParticles;

        public ParticleManager ParticleManager => ParticleManager ?? core.GetCoreComponent<ParticleManager>();

        /// <summary>
        /// 就目前而言，ModifyManager仅用于Block的情形下，对三种Modify(Damage, KnockBack, PoiseDamage)进行了收集
        /// </summary>
        public ModifyManager<Modifier<DamageData>, DamageData> DamageModifyManager => new();

        public void Damage(DamageData damageData)
        {
            print($"Damage Amount Before Modifiers: {damageData.Amount}");
            damageData = DamageModifyManager.ApplyAllModifiers(damageData);
            print($"Damage Amount After Modifiers: {damageData.Amount}");

            Health.Decrease(damageData.Amount);
            ParticleManager.StartParticlesWithRandomQuaternion(damageParticles);
        }

        protected override void Awake()
        {
            base.Awake();

            Health.Init();
        }
    }
}
