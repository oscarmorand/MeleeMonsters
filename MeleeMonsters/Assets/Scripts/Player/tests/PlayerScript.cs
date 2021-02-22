using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    internal PlayerInputs inputS;

    [SerializeField]
    internal PlayerMovements movementS;

    [SerializeField]
    internal PlayerCollision collisionS;

    public Rigidbody2D rb;

    public float moveSpeed;

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
        BasicHorizontalMovement();
    }

    void BasicHorizontalMovement()
    {
        float horizontalMovement = inputS.moveInput * moveSpeed * Time.deltaTime;
        movementS.HorizontalMovement(horizontalMovement);
    }
}
