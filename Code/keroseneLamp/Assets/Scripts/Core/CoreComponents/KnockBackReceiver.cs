using Assets.Scripts.Combat;
using Assets.Scripts.ModifierSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class KnockBackReceiver : CoreComponent, IKnockBackable
    {
        [SerialzeField] private float maxKnockBackTime = 0.2f;

        private float knockBackStartTime;
        private bool isKnockBackActive;

        public Movement Movement => Movement ?? core.GetCoreComponent<Movement>();

        public CollisionSenses CollisionSenses => CollisionSenses ?? core.GetCoreComponent<CollisionSenses>();

        public ModifyManager<Modifier<KnockBackData>, KnockBackData> KnockBackModifyManager = new();

        public void KnockBack(KnockBackData knockBackData)
        {
            knockBackData = KnockBackModifyManager.ApplyAllModifiers(knockBackData);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isKnockBackActive && ((Movement.CurrentVelocity.y < 0.01f && CollisionSenses.Ground)||Time.time > knockBackStartTime + maxKnockBackTime))
            {
                isKnockBackActive = false;
                Movement.CanSetVelocity = true;
            }
        }
    }
}
