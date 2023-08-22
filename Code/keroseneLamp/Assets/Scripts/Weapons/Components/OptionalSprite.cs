using UnityEngine;

namespace Assets.Scripts.Weapons
{
    /// <summary>
    /// 这个武器组件负责在攻击过程中显示一个可选的精灵
    /// OptionalSprite GameObject有一个精灵渲染器，其中显示这个可选的精灵。OptionalSprite游戏对象由基础动画放置到适当的位置
    /// SetOptionalSpriteEnabled（）和SetOptionalSpriteDisabled（）动画事件用于在适当的位置启用和禁用此可选精灵时间
    /// 正如标题所述，这个精灵是可选的，如果添加了组件，则不是每次攻击都需要。
    /// </summary>
    public class OptionalSprite : WeaponComponent<OptionalSpriteData, AttackOptionalSprite>
    {
        private SpriteRenderer spriteRenderer;

        private void HandleSetOptionalSpriteActive(bool value)
        {
            spriteRenderer.enabled = value;
        }

        protected override void HandleOnEnter()
        {
            base.HandleOnEnter();

            if (!currentAttackData.UseOptionalSprite) return;

            spriteRenderer.sprite = currentAttackData.Sprite;
        }

        #region Lifecycle
        protected override void Awake()
        {
            base.Awake();

            spriteRenderer = GetComponentInChildren<OptionalSpriteMarker>().SpriteRenderer;
            spriteRenderer.enabled = false;
        }

        protected override void Start()
        {
            base.Start();

            weapon.AnimationEventHandler.OnSetOptionalSpriteActive += HandleSetOptionalSpriteActive;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.AnimationEventHandler.OnSetOptionalSpriteActive -= HandleSetOptionalSpriteActive;
        }
        #endregion
    }
}
