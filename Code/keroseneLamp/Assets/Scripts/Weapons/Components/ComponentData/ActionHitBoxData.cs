using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    [RequireComponent(typeof(ActionHitBox))]
    public class ActionHitBoxData : ComponentData<AttackActionHitBox>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(ActionHitBox);
        }
    }
}
