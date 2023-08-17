namespace Assets.Scripts.Weapons
{
    public class TargeterData : ComponentData<AttackTargeter>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Targeter);
        }
    }
}
