using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerS;

    private Vector3 velocity = Vector3.zero;

    private int nbrJump;

    //private bool wallJumping;
    private bool wallSliding;
    public float wallJumpTime;

    // Start is called before the first frame update
    void Start()
    {
        nbrJump = playerS.maxJump;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HorizontalMovement()
    {
        float horizontalMovement = playerS.inputS.moveInput * playerS.moveSpeed * Time.deltaTime;
        Vector3 targetVelocity = new Vector2(horizontalMovement, playerS.rb.velocity.y);
        playerS.rb.velocity = Vector3.SmoothDamp(playerS.rb.velocity, targetVelocity, ref velocity, 0.05f);
    }

    public void Jump()
    {
        if (playerS.inputS.isJumpPressed && nbrJump > 0)
        {
            playerS.rb.velocity = new Vector2(playerS.rb.velocity.x, playerS.jumpForce);
            nbrJump--;
        }

        if (Input.GetButtonDown("Jump") && wallSliding)
        {
            //wallJumping = true;
            Invoke("SetWallJumpinToFalse", wallJumpTime);
        }
    }

    void SetWallJumpinToFalse()
    {
        //wallJumping = false;
    }
}
