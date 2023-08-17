using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    public class DirectTowardsTarget : ProjectileComponent
    {
        [field: SerializeField] public float MinStep { get; private set; }
        [field: SerializeField] public float MaxStep { get; private set; }
        [field: SerializeField] public float TimeToMaxStep { get; private set; }

        private IList<Transform> targeters;
        private Transform currentTargeter;

        private float step;
        private float startTime;

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not TargetsDataPackage targetsDataPackage) return;

            targeters = targetsDataPackage.Targets;
        }

        protected override void HandleInit()
        {
            base.HandleInit();

            currentTargeter = null;
            step = MinStep;
            startTime = Time.time;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!HasTarget()) return;

            step = Mathf.Lerp(MinStep, MaxStep, (Time.time - startTime) / TimeToMaxStep);
            var direction = (currentTargeter.position - transform.position).normalized;

            Rotate(direction);
        }

        private bool HasTarget()
        {
            if (currentTargeter) return true;

            if (targeters.Count <= 0) return false;

            // 取最近的一个
            targeters = targeters.OrderBy(target => (target.position - transform.position).sqrMagnitude).ToList();
            currentTargeter = targeters.FirstOrDefault();
            return true;
        }

        private void Rotate(Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, step * Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            if (!currentTargeter)
                return;

            Gizmos.DrawLine(transform.position, currentTargeter.position);
        }
    }
}
