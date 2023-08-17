using Assets.Scripts.Combat;
using Assets.Scripts.ModifierSystem;

namespace Assets.Scripts.Weapons
{
    public class BlockKnockBackModifier : Modifier<KnockBackData>
    {
        private readonly BlockConditionDelegate isKnockBacked;

        public BlockKnockBackModifier(BlockConditionDelegate isKnockBacked)
        {
            this.isKnockBacked = isKnockBacked;
        }

        public override KnockBackData ModifyValue(KnockBackData value)
        {
            if (isKnockBacked(value.Taker.transform, out var blockDirectionInformation))
            {
                value.Strength = value.Strength * (1 - blockDirectionInformation.KnockBackAbsorption);
            }

            return value;
        }
    }
}
