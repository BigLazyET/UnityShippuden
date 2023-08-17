using Assets.Scripts.Combat;
using Assets.Scripts.ModifierSystem;

namespace Assets.Scripts.Weapons
{
    public class BlockPoiseDamageModifier : Modifier<PoisonData>
    {
        private readonly BlockConditionDelegate isBlocked;

        public BlockPoiseDamageModifier(BlockConditionDelegate isBlocked)
        {
            this.isBlocked = isBlocked;
        }

        public override PoisonData ModifyValue(PoisonData value)
        {
            if (isBlocked(value.Taker.transform, out var blockDirectionInformation))
                value.SetAmount(value.Amount * (1 - blockDirectionInformation.PoiseDamageAbsorption));
            return value;
        }
    }
}
