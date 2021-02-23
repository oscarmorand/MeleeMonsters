using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private PhotonView photonView;

    [SerializeField]
    internal PlayerInput inputS;

    [SerializeField]
    internal PlayerMovements movementS;

    [SerializeField]
    internal PlayerCollision collisionS;

    public Rigidbody2D rb;

    public float moveSpeed;
    public int maxJump;
    public float jumpForce;

    private void Awake()
    {
        print("Main PlayerScript Awake");
    }


    void Start()
    {
        print("Main PlayerScript Starting");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(photonView.IsMine)
        {
            movementS.HorizontalMovement();
            movementS.Jump();
        }
    }

}
