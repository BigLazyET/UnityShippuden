using Assets.Scripts.CoreSystem;
using Assets.Scripts.SO;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// This script attach to Player GameObject directly
    /// </summary>
    public class Player : MonoBehaviour
    {
        [field: SerializeField] private PlayerDataSO playerDataSO;

        public Core Core { get; private set; }
        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        public BoxCollider2D MovementCollider { get; private set; }
        public Transform DashDirectionIndicator { get; private set; }
        public PoiseDamageReceiver PoiseDamageReceiver => PoiseDamageReceiver ?? Core.GetCoreComponent<PoiseDamageReceiver>();
        
        // player states
        public PlayerStateMachine StateMachine { get; private set; }

        // weapon
        private Weapon primaryWeapon;
        private Weapon secondaryWeapon;

        // workstation
        private Vector2 workspace;

        #region LifeCycle
        private void Awake()
        {
            // components
            Core = GetComponentInChildren<Core>();

            // player states
            StateMachine = new PlayerStateMachine();

            // weapon
            primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
            secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();
            primaryWeapon.SetCore(Core);
            secondaryWeapon.SetCore(Core);

            StateMachine.AddState(new PlayerIdleState(this, playerDataSO, StateMachine, "idle"));
            StateMachine.AddState(new PlayerMoveState(this, playerDataSO, StateMachine, "move"));
            //StateMachine.AddState(new PlayerJumpState(this, playerDataSO, StateMachine, "inAir"));
            StateMachine.AddState(new PlayerInAirState(this, playerDataSO, StateMachine, "inAir"));
            //StateMachine.AddState(new PlayerLandState(this, playerDataSO, StateMachine, "land"));
            //StateMachine.AddState(new PlayerWallSlideState(this, playerDataSO, StateMachine, "wallSlide"));
            //StateMachine.AddState(new PlayerWallGrabState(this, playerDataSO, StateMachine, "wallGrab"));
            //StateMachine.AddState(new PlayerWallClimbState(this, playerDataSO, StateMachine, "wallClimb"));
            StateMachine.AddState(new PlayerWallJumpState(this, playerDataSO, StateMachine, "inAir"));
            StateMachine.AddState(new PlayerLedgeClimbState(this, playerDataSO, StateMachine, "ledgeClimbState"));
            //StateMachine.AddState(new PlayerDashState(this, playerDataSO, StateMachine, "inAir"));
            StateMachine.AddState(new PlayerCrouchIdleState(this, playerDataSO, StateMachine, "crouchIdle"));
            //StateMachine.AddState(new PlayerCrouchMoveState(this, playerDataSO, StateMachine, "crouchMove"));
            //StateMachine.AddState(new PlayerAttackState(this, playerDataSO, StateMachine, "idle"));
            //StateMachine.AddState(new PlayerAttackState(this, playerDataSO, StateMachine, "idle"));
            //StateMachine.AddState(new PlayerStunState(this, playerDataSO, StateMachine, "stun"));

            //PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", primaryWeapon, CombatInputs.primary);
            //SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", secondaryWeapon, CombatInputs.secondary);
        }

        private void Start()
        {
            // components
            Animator = GetComponent<Animator>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            MovementCollider = GetComponent<BoxCollider2D>();
            InputHandler = GetComponent<PlayerInputHandler>();
            DashDirectionIndicator = transform.Find("DashDirectionIndicator");

            PoiseDamageReceiver.Poison.OnCurrentValueZero += HandleCurrentValueZero;

            StateMachine.Initialize(PlayerStateType.Idle);
        }

        private void Update()
        {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void OnDestroy()
        {
            PoiseDamageReceiver.Poison.OnCurrentValueZero -= HandleCurrentValueZero;
        }
        #endregion

        /// <summary>
        ///  重新调节碰撞器大小
        ///  用于潜行/蹲下的场景
        /// </summary>
        /// <param name="height"></param>
        public void SetColliderHeight(float height)
        {
            var center = MovementCollider.offset;   // 中心点
            workspace.Set(MovementCollider.size.x, height);

            MovementCollider.size = workspace;
            center.y += (height - MovementCollider.size.y) / 2;
            MovementCollider.offset = center;
        }

        private void HandleCurrentValueZero()
        {
            StateMachine.ChangeState(PlayerStateType.Stun);
        }
    }
}
