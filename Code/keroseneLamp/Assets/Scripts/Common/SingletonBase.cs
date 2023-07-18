using UnityEngine;

namespace Assets.Scripts.Common
{
    public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
    {
        private static T instance;
        public static T Instance => instance;

        private void Awake()
        {
            if (instance != null)
                Destroy(gameObject);
            instance = (T)this;
        }

        private void OnDestroy()
        {
            if(instance == this)
                instance = null;
        }
    }
}
