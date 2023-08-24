using UnityEngine;

namespace Assets.Scripts.ModifierSystem
{
    public abstract class ModifierBase
    {
        public float AddTime { get; set; } = Time.time;
    }
}
