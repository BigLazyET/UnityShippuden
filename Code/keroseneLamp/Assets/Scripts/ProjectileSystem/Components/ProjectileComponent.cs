using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    public class ProjectileComponent : MonoBehaviour
    {
        public Projectile projectile;

        public bool Active { get; private set; }

        protected virtual void HandleReset()
        {
            
        }

        protected virtual void HandleInit()
        {
            SetActive(true);
        }

        protected virtual void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            
        }

        protected void SetActive(bool value) => Active = value;

        #region Lifecycle
        protected virtual void Awake()
        {
            projectile = GetComponent<Projectile>();

            projectile.OnInit += HandleInit;
            projectile.OnReset += HandleReset;
            projectile.OnReceiveDataPackage += HandleReceiveDataPackage;
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
            
        }

        protected virtual void FixedUpdate()
        {
            
        }

        protected virtual void OnDestroy()
        {
            projectile.OnInit -= HandleInit;
            projectile.OnReset -= HandleReset;
            projectile.OnReceiveDataPackage -= HandleReceiveDataPackage;
        }
        #endregion
    }
}
