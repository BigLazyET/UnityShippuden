using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class DelayedGravity : ProjectileComponent
    {
        [field: SerializeField] public float Distance { get; private set; } = 10f;

        [HideInInspector] public float distanceMultipler = 1f;

        private Vector3 originPos;

        private float gravity;

        private void SetGravity()
        {
            projectile.Rigidbody2D.gravityScale = gravity;
        }

        protected override void HandleInit()
        {
            base.HandleInit();

            projectile.Rigidbody2D.gravityScale = 0f;
            distanceMultipler = 1f;

            originPos = transform.position;
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            var distance = Vector3.Distance(transform.position, originPos);
            if (distance > Distance * distanceMultipler)
                SetGravity();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
