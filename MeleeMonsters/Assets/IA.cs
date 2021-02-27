using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IA : MonoBehaviour
{
    PlayerMovement playerMovement;
    GameObject player;
    Transform playerTrans;
    float distanceX;
    float distanceY;
    float relativeSideX;
    float relativeSideY;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.dashInputy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) 
        {
            gameObject.tag = "IA";
            player = GameObject.FindGameObjectWithTag("Player");
            playerTrans = player.transform;
        } 
        else
        {
            relativeSideX = playerTrans.position.x - gameObject.transform.position.x;
            relativeSideY = playerTrans.position.y - gameObject.transform.position.y;
            distanceX = Math.Abs(relativeSideX);
            distanceY = Math.Abs(relativeSideY);

            playerMovement.dashInputx = playerMovement.direction;
            playerMovement.moveInputx = 0;

            //flip pour regarder dans la direction du player
            if (relativeSideX < 0.5f && playerMovement.facingRight)
                playerMovement.Flip();
            if (relativeSideX > 0.5f && !playerMovement.facingRight)
                playerMovement.Flip();

            if (playerMovement.isGrounded) //si l'IA est au sol
            {
                if (distanceX > 3.5f) //si le player est assez loin de l'IA (en x)
                {
                    if (relativeSideY < 3f) //si le player n'est pas trop haut par rapport à l'IA (en y)
                        playerMovement.DashState();
                    else
                    {
                        playerMovement.moveInputx = playerMovement.direction; //appelle MovePlayerUpdate
                        playerMovement.JumpState();
                    }
                }
            }
            else //si l'IA n'est pas au sol
            {
                if (playerMovement.wallSliding)
                {
                    playerMovement.WallJumpState();
                }

                playerMovement.moveInputx = playerMovement.direction;
                if (relativeSideY > 3f && playerMovement.nbrJump > 0) //si le player est au dessus de l'IA et qu'elle a des sauts
                {
                    playerMovement.JumpState();
                }
            }

            /*
            if (relativeSideY > 3f)
            {
                if (playerMovement.nbrJump > 0)
                    playerMovement.JumpState();
                else if (playerMovement.nbrDash > 0 && !playerMovement.isJumping)
                {
                    playerMovement.dashInputy = 1;
                    playerMovement.DashState();
                }
            }
            */
        }
    }
}
