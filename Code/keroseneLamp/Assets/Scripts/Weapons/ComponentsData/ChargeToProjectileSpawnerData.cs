namespace Assets.Scripts.Weapons
{
    public class ChargeToProjectileSpawnerData : ComponentData<AttackChargeToProjectileSpawner>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(ChargeToProjectileSpawner);
        }
    }
}
