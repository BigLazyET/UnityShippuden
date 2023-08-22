namespace Assets.Scripts.ObjectPoolSystem
{
    public interface IObjectPool<T> where T : class
    {
        T Get();
        void Return(T obj);
    }
}
