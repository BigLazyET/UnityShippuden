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
    /// �ܱ߼�飺�����飬ǽ�ڼ��
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
    /// ����û�����
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
    /// ����ɫ����
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

        // �ж��Ƿ�������
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
    /// ���¶���
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
    /// ��ɫ����ת
    /// </summary>
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(movementInputDirection, 1f, 1f);
        //transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    /// <summary>
    /// ��ɫ��Ծ
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
        // ���Ƶ�����ķ�ΧԲ
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        // ����ǽ�ڼ�������
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
