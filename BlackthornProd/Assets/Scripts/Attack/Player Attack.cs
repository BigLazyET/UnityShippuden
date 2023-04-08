using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;    // 当前帧的攻击间歇时间
    public float startTimeBtwAttack;    // 攻击间歇总时间

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0) // 攻击间歇结束，意味着可以进行下一轮攻击
        {
            if (Input.GetKey(KeyCode.Space))    // 按空格键攻击
            {
                animator.SetTrigger("attack");  // 播放攻击动画

                // https://blog.csdn.net/LCF_CSharp/article/details/123555319
            }
            
        }
    }
}
