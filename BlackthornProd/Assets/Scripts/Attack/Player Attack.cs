using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;    // ��ǰ֡�Ĺ�����Ъʱ��
    public float startTimeBtwAttack;    // ������Ъ��ʱ��

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0) // ������Ъ��������ζ�ſ��Խ�����һ�ֹ���
        {
            if (Input.GetKey(KeyCode.Space))    // ���ո������
            {
                animator.SetTrigger("attack");  // ���Ź�������

                // https://blog.csdn.net/LCF_CSharp/article/details/123555319
            }
            
        }
    }
}
