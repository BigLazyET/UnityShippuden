namespace Assets.Scripts.Weapons
{
    public class ProjectileSpawnerData : ComponentData<AttackProjectileSpawner>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(ProjectileSpawner);
        }
    }
}
