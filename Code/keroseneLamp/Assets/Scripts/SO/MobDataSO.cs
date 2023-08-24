using Assets.Scripts.EnemySystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "mobData", menuName = "Data/Enemy Data/Mob Data")]
    public class MobDataSO : ScriptableObject
    {
        private IList<EnemyComponentData> ComponentData = new List<EnemyComponentData>();

        public T GetData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }
    }
}
