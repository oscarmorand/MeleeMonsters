using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private PlayerMovement pM;

    private PlayerAnimation pA;

    private PlayerAttacks pAt;

    private PlayerScript pS;

    // Start is called before the first frame update
    void Start()
    {
        pM = GetComponent<PlayerMovement>();
        pA = GetComponent<PlayerAnimation>();
        pAt = GetComponent<PlayerAttacks>();
        pS = GetComponent<PlayerScript>();
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
        if (context.performed && !pS.isHitStun)
        {
            if (!(pM.isGrounded) && !(pM.isFastFalling) && !(pM.wallSliding))
            {
                pM.FastFallState();
            }
            pM.isPressingDown = true;
            pM.TransparentState();
        }
        else
            pM.isPressingDown = false;
    }


    public void DashInput(InputAction.CallbackContext context)
    {
        //test à supprimer si besoin (déjà fait dans pM)
        if (context.performed && pM.nbrDash > 0 && !pS.isHitStun)
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
        }
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed && !pS.isHitStun)
        {
            //test à supprimer si besoin (déjà fait dans pM)
            if (pM.nbrJump > 0)
            {
                pM.JumpState();
                pA.TakeOf();
            }
            if (pM.wallSliding)
            {
                pM.WallJumpState();
            }
        }
    }

    public void WrathModeInput (InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            pS.WrathModeState();
        }
    }

   public void NormalAttackInput (InputAction.CallbackContext context)
   {
        if (context.performed)
        {
            pAt.normalButton = true;
        }
        else
        {
            pAt.normalButton = false;
        }
   }

    public void SpecialAttackInput (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("special performed");
            pAt.specialButton = true;
            pAt.specialTimeStarted = Time.time;
            pAt.stoppedPressing = false;
        }
        if(context.canceled)
        {
            print("special canceled");
            pAt.specialButton = false;
            pAt.stoppedPressing = true;
            pAt.specialTimeFinished = Time.time;
        }
    }

}
