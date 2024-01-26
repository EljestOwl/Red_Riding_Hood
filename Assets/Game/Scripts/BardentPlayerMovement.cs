using System;
using UnityEngine;

public class Bardent : MonoBehaviour
{
    Rigidbody2D rigidbody2;
    Animator animator;
    [SerializeField]
    Transform groundCheckTransform;


    private float movementInputDirection;
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float jumpForce = 10f;
    private bool isJumping = false;
    private float groundCheckRadius = 0.2f;
    private bool isFacingRight;
    [SerializeField]
    LayerMask whatIsGround;

    private string currentState;
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";




    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isFacingRight = true;
        currentState = PLAYER_IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckIfShouldFlip();
        DiractionalAnimationState();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (movementInputDirection != 0f)
        {
            rigidbody2.velocity = new Vector2(movementInputDirection * movementSpeed, rigidbody2.velocity.y);
        }
        else
        {
            rigidbody2.velocity = new Vector2(0, rigidbody2.velocity.y);
        }
    }

    private void Jump()
    {
        if (CheckGrounded())
        {
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, jumpForce);
        }
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void CheckIfShouldFlip()
    {
        if(movementInputDirection > 0f && !isFacingRight)
        {
            Flip();
        }
        else if (movementInputDirection < 0f && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight =! isFacingRight;
        Vector3 localTransform = transform.localScale;
         localTransform.x *= -1;
        transform.localScale = localTransform;
    }

    private bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, whatIsGround);
    }

    private void ChangeAnimation(string newAnimation)
    {
        if (newAnimation == currentState)
        {
            return;
        }

        animator.Play(newAnimation);

        currentState = newAnimation;
    }

    private void DiractionalAnimationState()
    {
        if (CheckGrounded())
        {
            Debug.Log(rigidbody2.velocity);
            if (rigidbody2.velocity.x < -0.1f || rigidbody2.velocity.x > 0.1f)
            {
                ChangeAnimation(PLAYER_RUN);
            }
            else
            {
                ChangeAnimation(PLAYER_IDLE);
            }
        }
        if(!CheckGrounded())
        {
            ChangeAnimation(PLAYER_JUMP);
        }
    }
}
