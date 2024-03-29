﻿namespace Assets.Scripts.ProjectileSystem
{
    public class DrawModifyDelayedGravity : ProjectileComponent
    {
        private DelayedGravity delayedGravity;

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not DrawModifierDataPackage drawModifierDataPackage) return;

            delayedGravity.distanceMultipler = drawModifierDataPackage.DrawPercentage;
        }

        protected override void Awake()
        {
            base.Awake();

            delayedGravity = GetComponent<DelayedGravity>();
        }
    }
}
