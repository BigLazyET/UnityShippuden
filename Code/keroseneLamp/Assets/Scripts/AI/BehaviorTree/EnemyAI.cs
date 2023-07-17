using BT;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree.Demo1
{
    public class EnemyAI : BTTree
    {
        public Transform target;
        public List<Transform> positions;

        public override BTNode Init()
        {
            base.Init();

            var root = new BTSelector();

            var distanceLessThanChaseDis = new DistanceLessThanChaseDis(target, this.gameObject.transform);
            var distanceGreaterThanChaseDis = new DistanceGreaterThanChaseDis(target, this.gameObject.transform);

            var patrol = new Patrol(positions, this.gameObject.transform, 3);
            var chase = new Chase(target, this.gameObject.transform, 5);

            var tree2_1 = new BTSequence();
            tree2_1.AddChild(distanceLessThanChaseDis);
            tree2_1.AddChild(chase);

            var tree2_2 = new BTSequence();
            tree2_2.AddChild(distanceGreaterThanChaseDis);
            tree2_2.AddChild(patrol);

            root.AddChild(tree2_1);
            root.AddChild(tree2_2);

            return root;
        }
    }

    public class DistanceLessThanChaseDis : BTConditional
    {
        public float minChaseDistance = 5;
        public Transform target;
        public Transform trans;

        public DistanceLessThanChaseDis(Transform target, Transform trans)
        {
            this.trans = trans;
            this.target = target;
        }

        public override void Activate(BTDatabase database)
        {
            base.Activate(database);

            database.SetData("CHASESPEED", 5);
        }

        public override bool Check()
        {
            var distance = Vector3.Distance(target.position, trans.position);
            return distance < minChaseDistance;
        }
    }

    public class DistanceGreaterThanChaseDis : BTConditional
    {
        public float minChaseDistance = 5;
        public Transform target;
        public Transform trans;

        public DistanceGreaterThanChaseDis(Transform target, Transform trans)
        {
            this.trans = trans;
            this.target = target;
        }

        public override bool Check()
        {
            var distance = Vector3.Distance(target.position, trans.position);
            return distance > minChaseDistance;
        }
    }

    public class Patrol : BTAction
    {
        private int index;
        private float speed;
        private IList<Transform> positions;

        private Transform trans;

        public Patrol(List<Transform> positions, Transform trans, float speed)
        {
            this.positions = positions;
            this.speed = speed;
            this.trans = trans;
        }

        protected override BTResult Execute()
        {
            var distance = Vector3.Distance(positions[index].position, trans.position);
            if(distance < 0.1f)
            {
                index++;
                index %= positions.Count;
            }

            var direction = positions[index].position.normalized - trans.position.normalized;
            trans.position += direction * speed * Time.deltaTime;

            return BTResult.Success;
        }
    }

    public class Chase : BTAction
    {
        private Transform target;
        private Transform trans;
        private float speed;

        public Chase(Transform target, Transform trans, float speed)
        {
            this.target = target;
            this.trans = trans;
            this.speed = speed;
        }

        protected override BTResult Execute()
        {
            var direction = (target.position - trans.position).normalized;
            trans.position += direction * speed * Time.deltaTime;

            return BTResult.Success;
        }
    }
}
