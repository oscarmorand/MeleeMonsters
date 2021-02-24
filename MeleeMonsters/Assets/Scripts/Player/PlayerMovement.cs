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

    internal bool isJumping;
    private int maxJump;
    internal int nbrJump;
    private float jumpForce;

    internal float moveInputx;
    internal float moveInputy;
    private bool facingRight = true;
    internal float direction = 1;

    internal bool wallSliding;
    private float wallSlidingSpeed;

    private bool wallJumping;
    public float xWallForce;
    private float yWallForce;
    public float wallJumpTime;

    private bool isDashing;
    internal int nbrDash;
    private int maxDash;
    private float dashForce;
    private float dashTime;
    internal float dashInputx;
    internal float dashInputy;

    internal bool isFastFalling;
    private float fastFallingSpeed;

    private float moveSpeed;

    internal bool isGrounded;
    internal bool isTouchingFront;

 
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

    }


    private void Update()
    {
        if (photonView.IsMine)
        {

            if (isGrounded)
            {
                nbrJump = maxJump;
                nbrDash = maxDash;
                isFastFalling = false;
            }

            if (isTouchingFront && !isGrounded && moveInputx != 0)
                wallSliding = true;
            else
                wallSliding = false;

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




    public void WallJumpState()
    {
        wallJumping = true;
        Invoke("SetWallJumpinToFalse", wallJumpTime);
    }
    void SetWallJumpinToFalse()
    {
        wallJumping = false;
    }


    public void DashState()
    {
        isDashing = true;
        Invoke("SetDashingToFalse", dashTime);
    }
    void SetDashingToFalse()
    {
        isDashing = false;
    }

    public void FastFallState()
    {
        isFastFalling = true;
    }

}
