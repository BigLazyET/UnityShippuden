using Unity.Entities;
using Unity.Mathematics;

namespace PlayerController
{
    public struct DeferredCharacterImpulse
    {
        public Entity Entity;
        public float3 Impulse;
        public float3 Point;
    }
}
