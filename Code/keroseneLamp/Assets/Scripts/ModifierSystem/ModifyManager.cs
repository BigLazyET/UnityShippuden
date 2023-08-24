using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.ModifierSystem
{
    public class ModifyManager<TModifierType, TValueType> where TModifierType : Modifier<TValueType>
    {
        public IList<TModifierType> ModifierTypes = new List<TModifierType>();

        public void AddModifier(TModifierType modifierType)
        {
            modifierType.AddTime = Time.time;
            ModifierTypes.Add(modifierType);
        }

        public void RemoveModifier(TModifierType modifierType) => ModifierTypes.Remove(modifierType);

        public TValueType ApplyAllModifiers(TValueType initialValue)
        {
            var modifiedValue = initialValue;

            ModifierTypes = ModifierTypes.OrderBy(x => x.AddTime).ToList();

            foreach (var modifier in ModifierTypes)
                modifiedValue = modifier.ModifyValue(modifiedValue);

            return modifiedValue;
        }
    }
}
