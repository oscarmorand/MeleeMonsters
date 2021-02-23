using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    internal bool isJumpPressed;
    internal float moveInput;

    // Start is called before the first frame update
    void Start()
    {
        print("PlayerInputScript Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            print("jump ducon");
            isJumpPressed = true;
        }

        moveInput = Input.GetAxis("Horizontal");
    }
}
