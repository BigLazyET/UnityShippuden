namespace Assets.Scripts.Weapons
{
    public class MovementData : ComponentData<AttackMovement>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Movement);
        }
    }
}
