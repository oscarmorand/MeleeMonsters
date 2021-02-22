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

    [SerializeField]
    private MonsterScriptableObject settings;

    private int maxJump;
    private int nbrJump;

    private float moveInput;
    private bool facingRight = true;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    private float moveSpeed;
    private float jumpForce;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = settings.speed;
        jumpForce = settings.jumpStrength;
        maxJump = settings.extraJump;
        nbrJump = maxJump;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            if (isGrounded)
                nbrJump = maxJump;

            if (Input.GetButtonDown("Jump") && nbrJump > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                nbrJump--;
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
}
