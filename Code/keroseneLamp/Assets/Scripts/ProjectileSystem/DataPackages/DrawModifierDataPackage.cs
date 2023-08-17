using System;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    [Serializable]
    public class DrawModifierDataPackage : ProjectileDataPackage
    {
        private float drawPercentage;

        public float DrawPercentage { get { return drawPercentage; } set { drawPercentage = Mathf.Clamp01(value); } }
    }
}
