using Assets.Scripts.Combat;
using Assets.Scripts.ModifierSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class DamageReceiver : CoreComponent
    {
        // set in inspector
        [SerializeField] private GameObject damageParticles;

        private BodyStatus bodyStatus;

        public ModifyManager<Modifier<DamageData>, DamageData> DamageModifyManager => new();

        public override void Init()
        {
            base.Init();

            bodyStatus = core.GetCoreComponent<BodyStatus>();
        }
    }
}
