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

    private Animator anim;

    private bool isJumping;
    private int maxJump;
    private int nbrJump;
    private float jumpForce;

    private float moveInputx;
    private float moveInputy;
    private bool facingRight = true;
    private float direction = 1;

    private bool wallSliding;
    private float wallSlidingSpeed;

    private bool wallJumping;
    public float xWallForce;
    private float yWallForce;
    public float wallJumpTime;

    private bool isDashing;
    private int nbrDash;
    private int maxDash;
    private float dashForce;
    private float dashTime;
    private float dashInputx;
    private float dashInputy;

    private bool isFastFalling;
    private float fastFallingSpeed;

    private float moveSpeed;

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
        maxDash = settings.dashNbr;
        nbrDash = maxDash;
        dashTime = settings.dashTime;

        anim = GetComponent<Animator>();

    }


    private void Update()
    {
        if (photonView.IsMine)
        {
            CheckOverlap();

            if (isGrounded)
            {
                nbrJump = maxJump;
                nbrDash = maxDash;
                isFastFalling = false;
            }

            if (wallSliding)
            {
                nbrJump = maxJump;
                nbrDash = maxDash;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }

            if (wallJumping)
            {
                rb.velocity = new Vector2(xWallForce * -moveInputx, yWallForce);
            }

            if (isDashing)
            {
                rb.velocity = new Vector2(dashInputx * dashForce, (dashInputy * dashForce)/2);
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

            if(isGrounded)
            {
                if (moveInputx == 0)
                    anim.SetBool("isRunning", false);
                else
                    anim.SetBool("isRunning", true);
                anim.SetBool("isJumping", false);
            }
            else
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isJumping", true);
            }

            if (!facingRight && moveInputx > 0)
            {
                Flip();
            }
            else if (facingRight && moveInputx < 0)
            {
                Flip();
            }
        }
    }


    void MovePlayerUpdate()
    {
        float horizontalMovement = moveInputx * moveSpeed * Time.deltaTime;
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
        direction = -1 * direction;
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
            if (!isGrounded && !isFastFalling && !wallSliding)
            {
                FastFallState();
            }
            
        }
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        moveInputx = context.ReadValue<Vector2>().x;
        moveInputy = context.ReadValue<Vector2>().y;
    }



    public void DashInput(InputAction.CallbackContext context)
    {
        if (context.performed && nbrDash>0)
        {
            if(!isGrounded)
            {
                if(moveInputy != 0)
                {
                    if(moveInputx != 0)
                    {
                        dashInputx = moveInputx;
                    }
                    else
                    {
                        dashInputx = 0;
                    }
                    dashInputy = moveInputy;
                }
                else
                {
                    dashInputx = direction;
                    dashInputy = 0;
                }
            }
            else
            {
                dashInputx = direction;
                dashInputy = 0;
            }
            DashState();
            nbrDash--;
        }
    }



    public void JumpInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (nbrJump > 0)
            {
                isJumping = true;
                anim.SetTrigger("takeOf");
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
        if (isTouchingFront && !isGrounded && moveInputx != 0)
            wallSliding = true;
        else
            wallSliding = false;
        anim.SetBool("isWallSliding", wallSliding);
    }
}
