using Assets.Scripts.Combat;
using Assets.Scripts.ModifierSystem;
using UnityEngine;

namespace Assets.Scripts.CoreSystem
{
    public class KnockBackReceiver : CoreComponent, IKnockBackable
    {
        [SerializeField] private float maxKnockBackTime = 0.2f;

        private float knockBackStartTime;
        private bool isKnockBackActive;
        private Movement movement;
        private CollisionSenses collisionSenses;

        public ModifyManager<Modifier<KnockBackData>, KnockBackData> KnockBackModifyManager = new();

        public void KnockBack(KnockBackData knockBackData)
        {
            knockBackData = KnockBackModifyManager.ApplyAllModifiers(knockBackData);

            movement.SetVelocity(knockBackData.Strength, knockBackData.Angle, knockBackData.Direction);
            movement.CanSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTime = Time.time;
        }

        protected override void Awake()
        {
            base.Awake();

            movement = core.GetCoreComponent<Movement>();
            collisionSenses = core.GetCoreComponent<CollisionSenses>();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isKnockBackActive && ((movement.CurrentVelocity.y < 0.01f && collisionSenses.Ground)||Time.time > knockBackStartTime + maxKnockBackTime))
            {
                isKnockBackActive = false;
                movement.CanSetVelocity = true;
            }
        }
    }
}
