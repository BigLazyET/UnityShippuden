using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    int amountOfJumpsLeft;
    public float movementInputDirection;
    public bool isFacingRight = true;
    public bool isWalking;
    public bool isGrounded;
    public bool isCanJump;
    public bool isTouchingWall;
    public bool isWallSliding;

    public float movementSpeed = 10.0f;
    public float jumpForce = 7.0f;
    public float groundCheckRadius = 0.1f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform wallCheck;
    public int amountOfJumps = 1;
    public float wallCheckDistance = 0.4f;
    public float wallSlidingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfWallSliding();
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroudings();
        CheckCanJump();
    }

    /// <summary>
    /// 周边检查：地面检查，墙壁检查
    /// </summary>
    void CheckSurroudings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    void CheckCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (amountOfJumpsLeft <= 0)
        {
            isCanJump = false;
        }
        else
        {
            isCanJump = true;
        }
    }

    /// <summary>
    /// 检查用户输入
    /// </summary>
    void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    /// <summary>
    /// 检查角色朝向
    /// </summary>
    void CheckMovementDirection()
    {
        if (movementInputDirection > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }

        // 判断是否在行走
        if (rb.velocity.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    /// <summary>
    /// 更新动画
    /// </summary>
    void UpdateAnimations()
    {
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void ApplyMovement()
    {
        rb.velocity = new Vector2(movementInputDirection * movementSpeed, rb.velocity.y);

        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            }
        }
    }

    /// <summary>
    /// 角色朝向翻转
    /// </summary>
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(movementInputDirection, 1f, 1f);
        //transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    /// <summary>
    /// 角色跳跃
    /// </summary>
    void Jump()
    {
        if (isCanJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
    }

    void OnDrawGizmos()
    {
        // 绘制地面检测的范围圆
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        // 绘制墙壁检测的射线
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
