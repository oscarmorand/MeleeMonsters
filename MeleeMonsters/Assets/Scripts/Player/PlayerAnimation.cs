using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    internal Animator anim;

    PlayerScript playerScript;
    private PlayerMovement pM;
    private PhotonView pV;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        pM = GetComponent<PlayerMovement>();
        pV = GetComponent<PhotonView>();
        playerScript = GetComponent<PlayerScript>();
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

        if (playerScript.currentState == PlayerScript.States.Playing)
            DashAttack(pM.isDashAttacking);
    }

    public void TakeOf()
    {
        anim.SetTrigger("takeOf");
        pV.RPC("TakeOfRPC", RpcTarget.All);
    }

    [PunRPC]
    public void TakeOfRPC()
    {
        anim.SetTrigger("takeOf");
    }


    public void Attack(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void DashAttack(bool isDashAttacking)
    {
        anim.SetBool("isDashAttacking", isDashAttacking);
        pV.RPC("DashAttackRPC", RpcTarget.All, isDashAttacking);
    }

    [PunRPC]
    public void DashAttackRPC(bool isDashAttacking)
    {
        anim.SetBool("isDashAttacking", isDashAttacking);
    }
}
