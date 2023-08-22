using System;

namespace Assets.Scripts.ObjectPoolSystem
{
    public class DefaultPooledObjectPolicy<T> : PooledObjectPolicy<T> where T : class
    {
        public override T Create() => Activator.CreateInstance<T>();

        public override bool Return(T obj) => true;
    }
}
