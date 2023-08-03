namespace Assets.Scripts.ModifierSystem
{
    public abstract class Modifier<T> : ModifierBase
    {
        public abstract T ModifyValue(T value);
    }
}
