using System.Numerics;
using Unity.Entities;

namespace PlayerController
{
    /// <summary>
    /// 承担Player的Input信息
    /// 全局只有一个Entity拥有此Component
    /// </summary>
    public struct Input : IComponentData
    {
        public Vector2 movement;
        public Vector2 looking;
        public int jumped;
    }
}
