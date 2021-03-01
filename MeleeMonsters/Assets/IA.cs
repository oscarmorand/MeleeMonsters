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

                RaycastHit2D isThereGroundUnder = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector2.down), 100f, 1 << 8); //Le 1<<8 correspond au layerMask 8 (Ground)

                if (!isThereGroundUnder) //Si l'IA n'est pas au-dessus d'un sol
                {
                    RaycastHit2D isThereGroundLeft = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector2.left), 100f, 1 << 8);
                    RaycastHit2D isThereGroundRight = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector2.right), 100f, 1 << 8);
                    RaycastHit2D isThereGroundUnderLeft = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(new Vector2(-1, -1)), 100f, 1 << 8);
                    RaycastHit2D isThereGroundUnderRight = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(new Vector2(1, -1)), 100f, 1 << 8);

                    if (isThereGroundLeft || isThereGroundUnderLeft) //Si le terrain est à gauche de l'IA
                    {
                        if (playerMovement.nbrJump > 0)
                        {
                            playerMovement.moveInputx = -1;
                            playerMovement.JumpState(); //Saut vers la gauche
                        }
                        else if (playerMovement.nbrDash > 0 && !playerMovement.isJumping)
                        {
                            playerMovement.dashInputx = -1;
                            playerMovement.dashInputy = 1;
                            playerMovement.DashState(); //Dash vers le haut-gauche
                        }
                        else
                            playerMovement.moveInputx = -1; //Move vers la gauche
                    }
                    else if (isThereGroundRight || isThereGroundUnderRight) //Si le terrain est à droite de l'IA
                    {
                        if (playerMovement.nbrJump > 0)
                        {
                            playerMovement.moveInputx = 1;
                            playerMovement.JumpState(); //Saut vers la droite
                        }
                        else if (playerMovement.nbrDash > 0 && !playerMovement.isJumping)
                        {
                            playerMovement.dashInputx = 1;
                            playerMovement.dashInputy = 1;
                            playerMovement.DashState(); //Dash vers le haut-droit
                        }
                        else
                            playerMovement.moveInputx = 1; //Move vers la droite
                    }
                }
                else
                {
                    playerMovement.moveInputx = playerMovement.direction;
                    if (relativeSideY > 3f && playerMovement.nbrJump > 0) //si le player est au dessus de l'IA et qu'elle a des sauts
                    {
                        playerMovement.JumpState();
                    }
                }
            }
        }
    }
}
