namespace Assets.Scripts.ObjectPoolSystem
{
    public class DefaultObjectPoolProvider : ObjectPoolProvider
    {
        public override ObjectPool<T> Create<T>(IPooledObjectPolicy<T> policy) => new DefaultObjectPool<T>(policy);
    }
}
