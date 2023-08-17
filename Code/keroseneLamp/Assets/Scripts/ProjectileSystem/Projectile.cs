using System;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    /// <summary>
    /// 此类是 Projectile Component 和 生成Projectile的实体 之间的桥梁
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        // 以下event用作给 Projectile Component 通知用
        public event Action OnInit;
        public event Action OnReset;
        public event Action<ProjectileDataPackage> OnReceiveDataPackage;

        public Rigidbody2D Rigidbody2D { get; private set; }

        public void Init() => OnInit?.Invoke();

        public void Reset() => OnReset?.Invoke();

        public void SendDataPackage(ProjectileDataPackage projectileDataPackage) => OnReceiveDataPackage?.Invoke(projectileDataPackage);

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}
