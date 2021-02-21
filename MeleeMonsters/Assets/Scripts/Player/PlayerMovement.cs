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

    public bool isJumping;
    public bool isGrounded;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    private float moveSpeed;
    private float jumpForce;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = settings.speed;
        jumpForce = settings.jumpStrength;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                isJumping = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            BasicMovementInput();
        }
    }

    void BasicMovementInput()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);

        if(isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }
}
