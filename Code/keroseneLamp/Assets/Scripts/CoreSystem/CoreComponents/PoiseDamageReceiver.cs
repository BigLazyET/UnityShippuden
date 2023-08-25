using Assets.Scripts.Combat;
using Assets.Scripts.ModifierSystem;
using UnityEngine;

namespace Assets.Scripts.CoreSystem
{
    public class PoiseDamageReceiver : CoreComponent, IPoisonable
    {
        [field: SerializeField] public BodyStatu Poison { get; private set; }  // 理解：玩家的耐毒初始值，当耐毒值归零后，玩家处于晕厥状态Stun
        [field: SerializeField] public float PoisonRecoveryRate { get; private set; }

        public ModifyManager<Modifier<PoisonData>, PoisonData> PoiseDamageModifyManager => new();

        protected override void Awake()
        {
            base.Awake();

            Poison.Init();
        }

        private void Update()
        {
            if (Poison.CurrentValue.Equals(Poison.MaxValue)) return;

            Poison.Increase(PoisonRecoveryRate * Time.deltaTime);
        }

        public void PoisonDamage(PoisonData poisonData)
        {
            poisonData = PoiseDamageModifyManager.ApplyAllModifiers(poisonData);

            Poison.Decrease(poisonData.Amount);
        }
    }
}
