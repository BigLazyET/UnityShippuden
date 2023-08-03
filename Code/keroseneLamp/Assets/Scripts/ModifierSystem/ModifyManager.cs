using System.Collections.Generic;

namespace Assets.Scripts.ModifierSystem
{
    public class ModifyManager<TModifierType, TValueType> where TModifierType : Modifier<TValueType>
    {
        public IList<TModifierType> ModifierTypes = new List<TModifierType>();

        public void AddModifier(TModifierType modifierType) => ModifierTypes.Add(modifierType);

        public void RemoveModifier(TModifierType modifierType) => ModifierTypes.Remove(modifierType);

        public TValueType ApplyModifiers(TValueType initialValue)
        {
            var modifiedValue = initialValue;

            foreach (var modifier in ModifierTypes)
                modifiedValue = modifier.ModifyValue(modifiedValue);    // TODO: sort order is better

            return modifiedValue;
        }
    }
}
