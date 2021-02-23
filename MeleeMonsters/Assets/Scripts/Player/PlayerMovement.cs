using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PhotonView photonView;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    public MonsterScriptableObject settings;

    private bool isJumping;
    private int maxJump;
    private int nbrJump;

    private float moveInput;
    private bool facingRight = true;

    private bool wallSliding;
    private float wallSlidingSpeed;

    private bool wallJumping;
    public float xWallForce;
    private float yWallForce;
    public float wallJumpTime;

    private bool isDashing;
    private bool canDash;
    private float dashForce;
    public float dashTime;

    private bool isFastFalling;
    private float fastFallingSpeed;

    private float moveSpeed;
    private float jumpForce;

    public Transform groundCheck;
    public Transform frontCheck;

    public float checkRadius;

    public bool isGrounded;
    public bool isTouchingFront;

    public LayerMask whatIsGround;


    void Start()
    {
 
        moveSpeed = settings.speed;
        jumpForce = settings.jumpStrength;
        maxJump = settings.extraJump;
        nbrJump = maxJump;
        wallSlidingSpeed = settings.wallSlidingSpeed;
        yWallForce = settings.yWallForce;
        rb.gravityScale = settings.gravityScale;
        dashForce = settings.dashForce;
        fastFallingSpeed = settings.fastFallingSpeed;

    }


    private void Update()
    {
        if (photonView.IsMine)
        {
            CheckOverlap();

            if (isGrounded)
            {
                nbrJump = maxJump;
                canDash = true;
                isFastFalling = false;
            }

            if (wallSliding)
            {
                nbrJump = maxJump;
                canDash = true;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }

            if (wallJumping)
            {
                rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
            }

            if (isDashing)
            {
                rb.velocity = transform.right * (int)moveInput * dashForce;
                isFastFalling = false;
            }

            if(isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                nbrJump--;
                isJumping = false;
                isFastFalling = false;
            }

            if (isFastFalling)
            {
                rb.velocity = new Vector2(rb.velocity.x, -fastFallingSpeed);
            }
        }
    }


    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            MovePlayerUpdate();

            if (!facingRight && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight && moveInput < 0)
            {
                Flip();
            }
        }
    }


    void MovePlayerUpdate()
    {
        //moveInput = Input.GetAxis("Horizontal");
        float horizontalMovement = moveInput * moveSpeed * Time.deltaTime;
        Move(horizontalMovement);
    }


    void Move(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }




    void WallJumpState()
    {
        wallJumping = true;
        Invoke("SetWallJumpinToFalse", wallJumpTime);
    }
    void SetWallJumpinToFalse()
    {
        wallJumping = false;
    }


    void DashState()
    {
        isDashing = true;
        Invoke("SetDashingToFalse", dashTime);
    }
    void SetDashingToFalse()
    {
        isDashing = false;
    }

    void FastFallState()
    {
        isFastFalling = true;
    }



    public void FastFallInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!isGrounded && !isFastFalling)
            {
                FastFallState();
            }
            
        }
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().x;
    }

    public void DashInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (moveInput != 0 && canDash)
            {
                DashState();
            }
            canDash = false;
        }
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (nbrJump > 0)
            {
                isJumping = true;
            }
            if (wallSliding)
            {
                WallJumpState();
            }
        }
    }



    void CheckOverlap()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
        if (isTouchingFront && !isGrounded && moveInput != 0)
            wallSliding = true;
        else
            wallSliding = false;
    }
}
