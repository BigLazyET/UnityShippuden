using Assets.Scripts.Combat;
using Assets.Scripts.ModifierSystem;

namespace Assets.Scripts.CoreSystem
{
    public class PoiseDamageReceiver : CoreComponent, IPoisonable
    {
        private BodyStatus bodyStatus;

        public ModifyManager<Modifier<PoisonData>, PoisonData> PoiseDamageModifyManager => new();

        protected override void Awake()
        {
            base.Awake();

            bodyStatus = core.GetCoreComponent<BodyStatus>();
        }

        public void Poison(PoisonData poisonData)
        {
            poisonData = PoiseDamageModifyManager.ApplyAllModifiers(poisonData);

            bodyStatus.Poison.Decrease(poisonData.Amount);
        }
    }
}
