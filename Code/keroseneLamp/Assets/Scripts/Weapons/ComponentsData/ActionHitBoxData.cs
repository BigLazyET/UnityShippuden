using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    [RequireComponent(typeof(ActionHitBox))]
    public class ActionHitBoxData : ComponentData<AttackActionHitBox>
    {
        [field:SerializeField] public LayerMask DetectableLayer { get; private set; }

        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(ActionHitBox);
        }
    }
}
