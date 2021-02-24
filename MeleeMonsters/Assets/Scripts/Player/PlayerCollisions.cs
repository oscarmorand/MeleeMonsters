using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    private PlayerMovement pM;

    public Transform groundCheck;
    public Transform frontCheck;

    public LayerMask whatIsGround;

    public float checkRadius;

    // Start is called before the first frame update
    void Start()
    {
        pM = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckOverlap();
    }

    void CheckOverlap()
    {
        pM.isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        pM.isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
    }
}
