using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private PlayerMovement pM;

    private PlayerAnimation pA;

    // Start is called before the first frame update
    void Start()
    {
        pM = GetComponent<PlayerMovement>();
        pA = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        pM.moveInputx = context.ReadValue<Vector2>().x;
        pM.moveInputy = context.ReadValue<Vector2>().y;
    }


    public void FastFallInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!(pM.isGrounded) && !(pM.isFastFalling) && !(pM.wallSliding))
            {
                pM.FastFallState();
            }

        }
    }


    public void DashInput(InputAction.CallbackContext context)
    {
        if (context.performed && pM.nbrDash > 0)
        {
            if (!pM.isGrounded)
            {
                if (pM.moveInputy != 0)
                {
                    if (pM.moveInputx != 0)
                    {
                        pM.dashInputx = pM.moveInputx;
                    }
                    else
                    {
                        pM.dashInputx = 0;
                    }
                    pM.dashInputy = pM.moveInputy;
                }
                else
                {
                    pM.dashInputx = pM.direction;
                    pM.dashInputy = 0;
                }
            }
            else
            {
                pM.dashInputx = pM.direction;
                pM.dashInputy = 0;
            }
            pM.DashState();
            pM.nbrDash--;
        }
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (pM.nbrJump > 0)
            {
                pM.isJumping = true;
                pA.TakeOf();
            }
            if (pM.wallSliding)
            {
                pM.WallJumpState();
            }
        }
    }

}
