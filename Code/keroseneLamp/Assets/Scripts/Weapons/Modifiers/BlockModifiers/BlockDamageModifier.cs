using Assets.Scripts.Combat;
using Assets.Scripts.ModifierSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class BlockDamageModifier : Modifier<Combat.DamageData>
    {
        public event Action<GameObject> OnBlock;

        private readonly BlockConditionDelegate isBlocked;

        public BlockDamageModifier(BlockConditionDelegate isBlocked)
        {
            this.isBlocked = isBlocked;
        }

        public override Combat.DamageData ModifyValue(Combat.DamageData value)
        {
            if(isBlocked(value.Source.transform, out var blockDirectionInformation))
            {
                value.SetAmount(value.Amount * (1 - blockDirectionInformation.DamageAbsorption));
                OnBlock?.Invoke(value.Source);
            }
            return value;
        }
    }
}
