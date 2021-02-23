using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PhotonView photonView;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    public MonsterScriptableObject settings;

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

    }


    private void Update()
    {
        if (photonView.IsMine)
        {
            CheckOverlap();
            CheckInputs();

            if (isGrounded)
            {
                nbrJump = maxJump;
                canDash = true;
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
            }
        }
    }


    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            BasicMovementInput();

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


    void BasicMovementInput()
    {
        moveInput = Input.GetAxis("Horizontal");
        float horizontalMovement = moveInput * moveSpeed * Time.deltaTime;
        MovePlayer(horizontalMovement);
    }


    void MovePlayer(float _horizontalMovement)
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


    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        nbrJump--;
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
        canDash = false;
    }




    void CheckInputs()
    {
        CheckJumpInputs();
        CheckDashInputs();
    }

    void CheckDashInputs()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && moveInput != 0 && canDash)
        {
            DashState();
        }
    }

    void CheckJumpInputs()
    {
        if (Input.GetButtonDown("Jump") && nbrJump > 0)
        {
            Jump();
        }

        if (Input.GetButtonDown("Jump") && wallSliding)
        {
            WallJumpState();
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
