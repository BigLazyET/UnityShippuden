using UnityEngine;

namespace Assets.Scripts.EnemySystem
{
    public class PlayerCheckSense : MonoBehaviour
    {
        [SerializeField] private Transform playerCheck;

        private EntityData entityData;

        private void Awake()
        {
            entityData = gameObject.GetComponent<EnemyEntity>().MobDataSO.GetData<EntityData>();
        }

        public virtual bool CheckPlayerInMinAgroRange()
        {
            return Physics2D.Raycast(playerCheck.position, transform.right, entityData.MinAgroDistance, entityData.WhatIsPlayer);
        }

        public virtual bool CheckPlayerInMaxAgroRange()
        {
            return Physics2D.Raycast(playerCheck.position, transform.right, entityData.MaxAgroDistance, entityData.WhatIsPlayer);
        }

        public virtual bool CheckPlayerInCloseRangeAction()
        {
            return Physics2D.Raycast(playerCheck.position, transform.right, entityData.CloseRangeActionDistance, entityData.WhatIsPlayer);
        }
    }
}
