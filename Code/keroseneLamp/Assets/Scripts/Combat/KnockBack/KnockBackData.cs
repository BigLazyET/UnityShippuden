using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class KnockBackData
    {
        public float Strength { get; set; }

        public Vector2 Angle { get; private set; }

        public int Direction { get; private set; }

        public GameObject Taker { get; private set; }

        public KnockBackData(Vector2 angle, int direction, float strength, GameObject taker) 
        {
            Angle = angle;
            Direction = direction;
            Strength = strength;
            Taker = taker;
        }
    }
}
