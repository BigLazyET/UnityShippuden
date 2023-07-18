using UnityEngine;

namespace Assets.Scripts.MathsDemo
{
    public class MathsDemo : MonoBehaviour
    {
        public Transform transformA;
        public Transform transformB;

        private void Update()
        {
            // Unity坐标系采用左手法则：https://blog.csdn.net/weixin_43147385/article/details/124230124

            // 点乘可以判断出目标物体在我的前方还是后方。大于零在前方，小于零在后方。
            // 点乘可以用来计算夹角余弦值 => 有了角度我们就可以做矩形(向量差在X/Y轴上的投影)或扇形(余弦算出角度)索敌AI等
            // 叉乘可以判断出目标物体在我的左边还是右边。大于零在右方，小于零在左方。还可以判断在物体内部或者外部。
            // 叉乘可以用来计算平面法向量
            // 点乘求角度，叉乘求方向
            // 比如敌人再附近，点乘可以求出玩家面朝方向和敌人方向的夹角，叉乘可以得出左转还是右转更好的转向敌人

            RotateDemo(transformA);

            CalcuateDistance(transformA, transformB);

            LookAtDemo(transformA, transformB);

            CalcuateAngleOrRadianDemo(transformA, transformB);

            BForwardAOrNotDemo(transformA, transformB);


            // A -> B 的方向及单位向量
            var direction = (transformB.position - transformA.position).normalized;

            // A 向 B 进行移动
            transformA.position += direction * Time.deltaTime * 5;   // speed: 5
            
        }

        private void RotateDemo(Transform transformA)
        {
            // A 围绕Y轴旋转30度
            transformA.Rotate(0, 30, 0);

            var rotation = Quaternion.Euler(0, 30, 0);
            transformA.rotation = rotation;

            rotation = Quaternion.Euler(new Vector3(0, 30, 0));
            transformA.rotation = rotation;

            rotation = Quaternion.AngleAxis(30, Vector3.up);    // 创建一个基于Vector3.up(Y轴)旋转30度的旋转
            transformA.rotation *= rotation;
        }

        private void CalcuateDistance(Transform transformA, Transform transformB)
        {
            // A 到 B 的距离
            var distance = Vector3.Distance(transformA.position, transformB.position);

            var relativePos = transformB.position - transformA.position;
            distance = relativePos.magnitude;
        }

        private void LookAtDemo(Transform transformA, Transform transformB)
        {
            // A 看向 B
            transformA.LookAt(transformB);
            transformA.LookAt(transformB.position);
            // 采用四元数
            var relativePos = transformB.position - transformA.position;
            var rotation = Quaternion.LookRotation(relativePos, Vector3.up); // 第二个参数默认为Vector3.up
            transformA.rotation = rotation;
            // 通过上面的计算A 和 B 之间的角度/弧度
        }

        private void CalcuateAngleOrRadianDemo(Transform transformA, Transform transformB)
        {
            // A 和 B 之间的角度，以度为单位 angle
            Quaternion.Angle(transformA.rotation, transformB.rotation);

            // A 和 B 之间的弧度，以弧度为单位 radian
            Vector3 relativePos = transformB.position - transformA.position;
            var radian = Mathf.Atan2(relativePos.y, relativePos.x);
            // 弧度和度的关系：比如通过弧度得到度，或者反之
            radian = 1;
            var angle = radian * Mathf.Rad2Deg; // Mathf.Rad2Deg = 180 / Math.PI
            radian = angle * Mathf.Deg2Rad;

            // A 和 B 之间的角度
            angle = Vector3.Angle(transformA.position, transformB.position);
        }

        private void BForwardAOrNotDemo(Transform transformA, Transform transformB)
        {
            // 先计算出B想对A的位置信息
            // 再使用A正方向与相对方向两个向量做点乘的相关运算
            Vector3 relativePosition = transformB.position - transformA.position;
            Vector3 cubeForward = transformA.forward;

            // 计算两个向量的点乘
            // 如果大于0说明B在A前面
            // 如果小于0说明B在A后面
            // 如果等于0说明B在A左右
            float result = Vector3.Dot(cubeForward, relativePosition);  // 点积

            // 得到两个向量后，可以直接计算其夹角
            float angle = Vector3.Angle(cubeForward, relativePosition);
            Debug.Log("两个向量的夹角：" + angle);

            // 这是前面说到的当两个向量的长度都为1时，点乘的结果就是夹角的余弦值
            float cos = Vector3.Dot(cubeForward.normalized, relativePosition.normalized);
            Debug.Log("余弦值：" + cos);

            // 通过反余弦函数得到两个向量的角度
            // 不过这里得到是弧度值，并不是角度值
            float radians = Mathf.Acos(cos);
            Debug.Log("通过余弦值求弧度：" + radians);

            // 弧度值通过数据库转换成角度值
            angle = radians * Mathf.Rad2Deg;
            Debug.Log("把弧度转换成角度：" + angle);
        }
    }
}
