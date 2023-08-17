using Assets.Scripts.CoreSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Block : WeaponComponent<BlockData, AttackBlock>
    {
        public event Action<GameObject> OnBlock;    // 参数是被Blocked的物体

        private DamageReceiver damageReceiver;
        private KnockBackReceiver knockBackReceiver;
        private PoiseDamageReceiver poiseDamageReceiver;

        private BlockDamageModifier blockDamageModifier;
        private BlockKnockBackModifier blockKnockBackModifier;
        private BlockPoiseDamageModifier blockPoiseDamageModifier;

        private Movement movement;

        protected override void Start()
        {
            base.Start();

            movement = Core.GetCoreComponent<Movement>();

            damageReceiver = Core.GetCoreComponent<DamageReceiver>();
            knockBackReceiver = Core.GetCoreComponent<KnockBackReceiver>();
            poiseDamageReceiver = Core.GetCoreComponent<PoiseDamageReceiver>();

            AnimationEventHandler.OnStartAnimationWindow += HandleStartAnimationWindow;
            AnimationEventHandler.OnStopAnimationWindow += HandleStopAnimationWindow;

            blockDamageModifier = new BlockDamageModifier(IsAttackBlocked);
            blockKnockBackModifier = new BlockKnockBackModifier(IsAttackBlocked);
            blockPoiseDamageModifier = new BlockPoiseDamageModifier(IsAttackBlocked);

            blockDamageModifier.OnBlock += HandleBlock;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            AnimationEventHandler.OnStartAnimationWindow -= HandleStartAnimationWindow;
            AnimationEventHandler.OnStopAnimationWindow -= HandleStopAnimationWindow;

            blockDamageModifier.OnBlock -= HandleBlock;
        }

        private void HandleStartAnimationWindow(AnimationWindow window)
        {
            if (window != AnimationWindow.Block) return;

            damageReceiver.DamageModifyManager.AddModifier(blockDamageModifier);
            knockBackReceiver.KnockBackModifyManager.AddModifier(blockKnockBackModifier);
            poiseDamageReceiver.PoiseDamageModifyManager.AddModifier(blockPoiseDamageModifier);
        }

        private void HandleStopAnimationWindow(AnimationWindow window)
        {
            if (window != AnimationWindow.Block) return;

            damageReceiver.DamageModifyManager.RemoveModifier(blockDamageModifier);
            knockBackReceiver.KnockBackModifyManager.RemoveModifier(blockKnockBackModifier);
            poiseDamageReceiver.PoiseDamageModifyManager.RemoveModifier(blockPoiseDamageModifier);
        }

        private void HandleBlock(GameObject source) => OnBlock?.Invoke(source);

        private bool IsAttackBlocked(Transform source, out BlockDirectionInformation blockDirectionInformation)
        {
            var angleOfAttack = Vector2.SignedAngle(Vector2.right * movement.FacingDirection,
                                                    (source.position-Core.Root.transform.position) * movement.FacingDirection);
            return currentAttackData.IsBlocked(angleOfAttack, out blockDirectionInformation);
        }
    }
}
