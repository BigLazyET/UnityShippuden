using Assets.Scripts.Combat;
using Assets.Scripts.ModifierSystem;

namespace Assets.Scripts.CoreSystem
{
    public class PoiseDamageReceiver : CoreComponent, IPoisonable
    {
        private BodyStatus bodyStatus;

        public ModifyManager<Modifier<PoisonData>, PoisonData> ModifyManager => new();

        public override void Init()
        {
            base.Init();

            bodyStatus = core.GetCoreComponent<BodyStatus>();
        }

        public void Poison(PoisonData poisonData)
        {
            poisonData = ModifyManager.ApplyAllModifiers(poisonData);

            bodyStatus.Poison.Decrease(poisonData.Amount);
        }
    }
}
