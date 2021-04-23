using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    internal Animator anim;

    private PlayerMovement pM;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        pM = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pM.isGrounded || pM.isOnPlatform)
        {
            if (pM.moveInputx == 0)
                anim.SetBool("isRunning", false);
            else
                anim.SetBool("isRunning", true);
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", true);
        }
           
        anim.SetBool("isWallSliding", pM.wallSliding);
    }

    public void TakeOf()
    {
        anim.SetTrigger("takeOf");
    }

    public void Attack(string trigger)
    {
        anim.SetTrigger(trigger);
    }
}
