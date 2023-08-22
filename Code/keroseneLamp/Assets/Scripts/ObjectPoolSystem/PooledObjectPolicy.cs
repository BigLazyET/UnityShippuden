namespace Assets.Scripts.ObjectPoolSystem
{
    public abstract class PooledObjectPolicy<T> : IPooledObjectPolicy<T> where T : class
    {
        public abstract T Create();

        public abstract bool Return(T obj);
    }
}
