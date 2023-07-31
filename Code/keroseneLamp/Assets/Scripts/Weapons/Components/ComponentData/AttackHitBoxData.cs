using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    [RequireComponent(typeof(ActionHitBox))]
    public class AttackHitBoxData : ComponentAttackData<AttackActionHitBox>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(ActionHitBox);
        }
    }
}
