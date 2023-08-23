using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class KnockBackData
    {
        /// <summary>
        /// 击退力度
        /// </summary>
        public float Strength { get; set; }

        /// <summary>
        /// 击退角度
        /// </summary>
        public Vector2 Angle { get; private set; }

        /// <summary>
        /// 击退方向
        /// </summary>
        public int Direction { get; private set; }

        /// <summary>
        /// 击退发起者 - 击退来源
        /// </summary>
        public GameObject Source { get; private set; }

        public KnockBackData(Vector2 angle, int direction, float strength, GameObject source) 
        {
            Angle = angle;
            Direction = direction;
            Strength = strength;
            Source = source;
        }
    }
}
