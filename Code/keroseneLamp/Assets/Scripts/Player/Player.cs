using Assets.Scripts.Core;
using Assets.Scripts.SO;
using Assets.Scripts.Weapons;
using UnityEngine;
using CoreNs = Assets.Scripts.Core;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// This script attach to Player GameObject directly
    /// </summary>
    public class Player : MonoBehaviour
    {
        // set in inspector
        [field: SerializeField] private PlayerDataSO playerDataSO;

        // components
        public CoreNs.Core Core { get; private set; }
        public BodyStatus BodyStatus { get; private set; }
        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        public BoxCollider2D MovementCollider { get; private set; }
        public Transform DashDirectionIndicator { get; private set; }
        
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
            Core = GetComponentInChildren<CoreNs.Core>();
            BodyStatus = Core.GetCoreComponent<BodyStatus>();

            // player states
            StateMachine = new PlayerStateMachine();

            // weapon
            primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
            secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();
            primaryWeapon.SetCore(Core);
            secondaryWeapon.SetCore(Core);

            StateMachine.AddState(new PlayerIdleState(this, playerDataSO, StateMachine, "idle"));
        }

        private void Start()
        {
            // components
            Animator = GetComponent<Animator>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            MovementCollider = GetComponent<BoxCollider2D>();
            InputHandler = GetComponent<PlayerInputHandler>();
            DashDirectionIndicator = transform.Find("DashDirectionIndicator");

            BodyStatus.Poison.OnCurrentValueZero += Poison_OnCurrentValueZero;

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
            BodyStatus.Poison.OnCurrentValueZero -= Poison_OnCurrentValueZero;
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

        private void Poison_OnCurrentValueZero()
        {
            StateMachine.ChangeState(PlayerStateType.Stun);
        }
    }
}
