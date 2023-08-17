
namespace Assets.Scripts.Weapons
{
    public class BlockData : ComponentData<AttackBlock>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Block);
        }
    }
}
