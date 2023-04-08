using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigi;
    Animator animator;

    [SerializeField] float speedX = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Direction();
        Movement();
        
    }

    void Direction()
    {
        var direction = Input.GetAxisRaw("Horizontal");
        //if (direction > 0)
        //    this.transform.eulerAngles = new Vector3(-1, 1, 1);
        //else
        //    this.transform.eulerAngles = new Vector3(1, 1, 1);

        var localScaleX = Mathf.Abs(this.transform.localScale.x);
        if (direction != 0)
            transform.localScale = new Vector3(-direction * localScaleX, this.transform.localScale.y, 1);
    }

    void Movement()
    {
        var moveX = Input.GetAxis("Horizontal");
        if (moveX != 0)
        {
            rigi.velocity = new Vector2(moveX * speedX, rigi.velocity.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
