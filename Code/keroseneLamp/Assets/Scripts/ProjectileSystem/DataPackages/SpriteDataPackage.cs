using System;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    [Serializable]
    public class SpriteDataPackage : ProjectileDataPackage
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
