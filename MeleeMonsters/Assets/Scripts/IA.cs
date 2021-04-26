using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IA : MonoBehaviour
{
    PlayerMovement playerMovement;
    GameObject player;
    Transform playerTrans;

    PlayerAttacks playerAttacks;
    PlayerScript playerScript;

    float distanceX;
    float distanceY;
    float relativeSideX;
    float relativeSideY;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.dashInputy = 0;

        playerAttacks = GetComponent<PlayerAttacks>();
        playerScript = GetComponent<PlayerScript>();
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
            if (relativeSideX < 1f && playerMovement.facingRight)
                playerMovement.Flip();
            if (relativeSideX > 1f && !playerMovement.facingRight)
                playerMovement.Flip();

            //utiliser le wrath mode si la barre est pleine
            if (playerScript.loadingWrath >= playerScript.maxLoadingWrath)
            {
                playerScript.WrathModeState();
            }

            if (playerMovement.isGrounded || playerMovement.isOnPlatform) //si l'IA est au sol ou sur une plateforme
            {
                if (playerMovement.isOnPlatform && relativeSideY <= -2.5f) //si l'IA est sur une plateforme et que le joueur est assez bas
                {
                    playerMovement.FastFallState();
                    playerMovement.TransparentState();
                }

                /*
                if (distanceX < 1.5f) //si le player est assez proche de l'IA (en x)
                {
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG);
                }
                */

                if (distanceX > 2.5f) //si le player est un peu loin de l'IA (en x)
                {
                    playerMovement.moveInputx = playerMovement.direction; //si pas très loin, aller vers le joueur

                    if (distanceX > 5f) //Si très loin, dash vers le joueur
                        DirectionalDash((int)playerMovement.direction, 0);

                    if (relativeSideY >= 2.5f) //si le player est trop haut par rapport à l'IA
                        playerMovement.JumpState();
                }
            }
            else //si l'IA n'est pas au sol et pas sur une plateforme
            {
                if (playerMovement.wallSliding)
                {
                    playerMovement.WallJumpState();
                }

                RaycastHit2D isThereGroundUnder = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector2.down), 100f, 1 << 8 | 1 << 10);
                //Le 1<<8 correspond au layerMask 8 (Ground) ou 10 (Plateforme)

                if (!isThereGroundUnder) //Si l'IA n'est pas au-dessus d'un sol ou d'une plateforme
                {
                    RaycastHit2D isThereGroundLeft = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector2.left), 100f, 1 << 8);
                    RaycastHit2D isThereGroundRight = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector2.right), 100f, 1 << 8);
                    RaycastHit2D isThereGroundUnderLeft = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(new Vector2(-1, -1)), 100f, 1 << 8);
                    RaycastHit2D isThereGroundUnderRight = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(new Vector2(1, -1)), 100f, 1 << 8);

                    if (isThereGroundLeft || isThereGroundUnderLeft) //Si le terrain est à gauche de l'IA
                    {
                        if (playerMovement.nbrJump > 0)
                            DirectionalJump(-1); //Saut vers la gauche
                        else if (playerMovement.nbrDash > 0 && !playerMovement.isJumping)
                            DirectionalDash(-1, 1); //Dash vers le haut-gauche
                        else
                            playerMovement.moveInputx = -1; //Move vers la gauche
                    }
                    else if (isThereGroundRight || isThereGroundUnderRight) //Si le terrain est à droite de l'IA
                    {
                        if (playerMovement.nbrJump > 0)
                            DirectionalJump(1); //Saut vers la droite
                        else if (playerMovement.nbrDash > 0 && !playerMovement.isJumping)
                            DirectionalDash(1, 1); //Dash vers le haut-droit
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

    private void DirectionalJump(int x)
    {
        if (x == -1 || x == 1)
        {
            playerMovement.moveInputx = x;
            playerMovement.JumpState();
        }
    }

    private void DirectionalDash(int x, int y)
    {
        if ((x >= -1 && x <= 1) && (y >= -1 && y <= 1))
        {
            playerMovement.dashInputx = x;
            playerMovement.dashInputy = y;
            playerMovement.DashState();
        }
    }
}
