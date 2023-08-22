using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Assets.Scripts.ObjectPoolSystem
{
    public class DefaultObjectPool<T> : ObjectPool<T> where T : class
    {
        private T firstItem;
        private IPooledObjectPolicy<T> policy;
        private PooledObjectPolicy<T> fastPolicy;
        private bool isDefaultPooledObjectPolicy;
        private ConcurrentQueue<T> items;

        public DefaultObjectPool(IPooledObjectPolicy<T> policy)
        {
            this.policy = policy;
            fastPolicy = policy as PooledObjectPolicy<T>;
            isDefaultPooledObjectPolicy = IsDefaultPolicy();
            items = new ConcurrentQueue<T>();

            bool IsDefaultPolicy()
            {
                return policy.GetType().IsGenericType && policy.GetType().GetGenericTypeDefinition() == typeof(DefaultObjectPool<>);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T Create() => fastPolicy?.Create() ?? policy?.Create();

        public override T Get()
        {
            if(items.TryDequeue(out T item))
                return item;

            item = Create();
            items.Enqueue(item);
            return item;
        }

        public override void Return(T item)
        {
            items.Enqueue(item);
        }
    }
}
