using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private PhotonView photonView;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    private MonsterScriptableObject settings;

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
    }


    private void Update()
    {
        if (photonView.IsMine)
        {
            CheckOverlap();
            CheckJumpInputs();

            if(wallSliding)
            {
                nbrJump = maxJump;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, - wallSlidingSpeed, float.MaxValue));
            }

            if(wallJumping)
            {
                rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
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


    void SetWallJumpinToFalse()
    {
        wallJumping = false;
    }


    void CheckJumpInputs()
    {
        if (Input.GetButtonDown("Jump") && nbrJump > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            nbrJump--;
        }

        if (Input.GetButtonDown("Jump") && wallSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpinToFalse", wallJumpTime);
        }
    }

    void CheckOverlap()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded)
            nbrJump = maxJump;

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
        if (isTouchingFront && !isGrounded && moveInput != 0)
            wallSliding = true;
        else
            wallSliding = false;
    }
}
