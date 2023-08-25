using Assets.Scripts.CoreSystem;
using Assets.Scripts.SO;
using UnityEngine;

namespace Assets.Scripts.EnemySystem
{
    [RequireComponent(typeof(PlayerCheckSense))]
    public class EnemyEntity : MonoBehaviour
    {
        [field: SerializeField] public EnemyDataSO MobDataSO { get; private set; }

        public Core Core { get; private set; }
        public Animator Animator { get; private set; }
        public EnemyStateMachine StateMachine { get; private set; }
        public Movement Movement => Movement ?? Core.GetCoreComponent<Movement>();
        public PoiseDamageReceiver PoiseDamageReceiver => PoiseDamageReceiver ?? Core.GetCoreComponent<PoiseDamageReceiver>();
        public PlayerCheckSense PlayerCheckSense { get; private set; }

        public EntityData entityData;

        private float lastDamageTime;
        private bool isStunned;
        private float currentStunResistance;

        public virtual void Awake()
        {
            Core = GetComponentInChildren<Core>();

            Animator = GetComponent<Animator>();
            PlayerCheckSense = GetComponent<PlayerCheckSense>();

            entityData = MobDataSO.GetData<EntityData>();
            StateMachine = new EnemyStateMachine();

            currentStunResistance = entityData.StunResistance;
        }

        public virtual void Update()
        {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();

            Animator.SetFloat("yVelocity", Movement.RB.velocity.y); // TODO?

            if (Time.time > lastDamageTime + entityData.StunRecoveryTime)
                ResetStunResistance();
        }

        public virtual void FixedUpdate() => StateMachine.CurrentState.PhysicsUpdate();

        /// <summary>
        /// 伤害跳跃
        /// </summary>
        public virtual void DamageHop(float velocity) => Movement.RB.velocity = new Vector2(Movement.RB.velocity.x, velocity);

        private void ResetStunResistance()
        {
            isStunned = false;
            currentStunResistance = entityData.StunResistance;
        }
    }
}
