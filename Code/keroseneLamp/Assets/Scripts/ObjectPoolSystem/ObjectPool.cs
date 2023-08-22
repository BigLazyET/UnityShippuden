namespace Assets.Scripts.ObjectPoolSystem
{
    public abstract class ObjectPool<T> : IObjectPool<T> where T : class
    {
        public abstract T Get();

        public abstract void Return(T obj);
    }

    public static class ObjectPool
    {
        public static ObjectPool<T> Create<T>(IPooledObjectPolicy<T> policy = null) where T : class, new()
            => new DefaultObjectPoolProvider().Create<T>(policy ?? new DefaultPooledObjectPolicy<T>());
    }
}
