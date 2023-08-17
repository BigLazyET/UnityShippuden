using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    [Serializable]
    public class TargetsDataPackage : ProjectileDataPackage
    {
        public IList<Transform> Targets { get; private set; }
    }
}
