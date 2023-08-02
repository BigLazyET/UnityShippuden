using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Movement : CoreComponent
    {
        public Rigidbody2D RB {  get; private set; }

        public int FacingDirection { get; private set; }

        public bool CanSetVelocity { get; private set; }

        public Vector2 CurrentVelocity { get; private set; }

        public Vector2 workspace;

        protected override void Awake()
        {
            base.Awake();

            RB = GetComponentInParent<Rigidbody2D>();
            FacingDirection = 1;
            CanSetVelocity = true;
        }

        public override void LogicUpdate()
        {
            CurrentVelocity = RB.velocity;
        }


    }
}
