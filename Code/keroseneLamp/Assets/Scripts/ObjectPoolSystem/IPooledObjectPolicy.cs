using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ObjectPoolSystem
{
    public interface IPooledObjectPolicy<T> where T: class
    {
        T Create();
        bool Return(T obj);
    }
}
