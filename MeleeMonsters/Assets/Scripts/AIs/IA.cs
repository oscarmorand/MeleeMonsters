using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IA : MonoBehaviour
{
    public GameManager.IADifficultyChoice iaDifficultyChoice;

    PlayerMovement playerMovement;
    GameObject player;
    Transform playerTrans;

    PlayerAttacks playerAttacks;
    PlayerScript playerScript;
    public IAAttacks iaattacks;
    private Rigidbody2D rb;

    public float distanceX;
    public float distanceY;
    public float relativeSideX;
    public float relativeSideY;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.dashInputy = 0;

        playerAttacks = GetComponent<PlayerAttacks>();
        playerScript = GetComponent<PlayerScript>();
        rb = GetComponent<Rigidbody2D>();
        GameObject manager = GameObject.Find("GameManagerPrefab").gameObject;
        GameManager gameManager = manager.GetComponent<GameManager>();
        iaDifficultyChoice = gameManager.iaLevel;

        //iaDifficultyChoice = "POUR LIANE"
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.currentState == PlayerScript.States.Frozen)
            return;

        rb.simulated = true;

        if (player == null) 
        {
            gameObject.tag = "IA";
            player = GameObject.FindGameObjectWithTag("Player");
            playerTrans = player.transform;
        } 
        else
        {
            System.Random rd = new System.Random();
            
            relativeSideX = playerTrans.position.x - gameObject.transform.position.x;
            relativeSideY = playerTrans.position.y - gameObject.transform.position.y;
            distanceX = Math.Abs(relativeSideX);
            distanceY = Math.Abs(relativeSideY);

            playerMovement.dashInputx = playerMovement.direction;
            playerMovement.moveInputx = 0;

            //flip pour regarder dans la direction du player
            if (relativeSideX < 0.75f && playerMovement.facingRight)
                playerMovement.Flip();
            if (relativeSideX > 0.75f && !playerMovement.facingRight)
                playerMovement.Flip();

            //utiliser le wrath mode si la barre est pleine
            if (playerScript.loadingWrath >= playerScript.maxLoadingWrath)
                playerScript.GoIntoWrathMode();

            if (playerMovement.isGrounded || playerMovement.isOnPlatform) //si l'IA est au sol ou sur une plateforme
            {
                if (playerMovement.isOnPlatform && relativeSideY <= -2.5f) //si l'IA est sur une plateforme et que le joueur est assez bas
                    FallFromPlatform();

                if (iaDifficultyChoice != GameManager.IADifficultyChoice.Dummy) //Si l'IA est autre chose qu'un dummy
                    FollowPlayerOnGround();

                if (iaDifficultyChoice == GameManager.IADifficultyChoice.Beginner)
                {
                    if (rd.Next(0, 30) == 0) //2 attaques par seconde
                    {
                        if (!playerScript.isWrath)
                            iaattacks.UseGroundAttacks();
                        else
                            iaattacks.UseGroundAttacksW();
                    }
                }

                if (iaDifficultyChoice == GameManager.IADifficultyChoice.Intermediate)
                {
                    if (rd.Next(0, 15) == 0) //4 attaques par seconde
                    {
                        if (!playerScript.isWrath)
                            iaattacks.UseGroundAttacks();
                        else
                            iaattacks.UseGroundAttacksW();
                    }
                }

                if (iaDifficultyChoice == GameManager.IADifficultyChoice.Advanced)
                {
                    if (!playerScript.isWrath)
                        iaattacks.UseGroundAttacks();
                    else
                        iaattacks.UseGroundAttacksW();
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
                    RecoverToTerrain();
                }
                else
                {
                    if (iaDifficultyChoice != GameManager.IADifficultyChoice.Dummy) //Si l'IA est autre chose qu'un dummy
                    {
                        if (relativeSideY > 4f && playerMovement.nbrJump > 0) //si le player est au dessus de l'IA et qu'elle a des sauts
                            DirectionalJump((int)playerMovement.direction);
                    }

                    if (iaDifficultyChoice == GameManager.IADifficultyChoice.Beginner)
                    {
                        if (rd.Next(0, 30) == 0) //2 attaques par seconde
                        {
                            if (!playerScript.isWrath)
                                iaattacks.UseAirAttacks();
                            else
                                iaattacks.UseAirAttacksW();
                        }
                    }

                    if (iaDifficultyChoice == GameManager.IADifficultyChoice.Intermediate)
                    {
                        if (rd.Next(0, 15) == 0) //4 attaques par seconde
                        {
                            if (!playerScript.isWrath)
                                iaattacks.UseAirAttacks();
                            else
                                iaattacks.UseAirAttacksW();
                        }
                    }

                    if (iaDifficultyChoice == GameManager.IADifficultyChoice.Advanced)
                    {
                        if (!playerScript.isWrath)
                            iaattacks.UseAirAttacks();
                        else
                            iaattacks.UseAirAttacksW();
                    }
                    
                }
            }
        }
    }

    private void DirectionalJump(int x)
    {
        if ((x == -1 || x == 1) && playerMovement.nbrJump > 0)
        {
            playerMovement.moveInputx = x;
            playerMovement.JumpState();
        }
    }

    private void DirectionalDash(int x, int y)
    {
        if ((x >= -1 && x <= 1) && (y >= -1 && y <= 1) && playerMovement.nbrDash > 0)
        {
            playerMovement.dashInputx = x;
            playerMovement.dashInputy = y;
            playerMovement.DashState();
        }
    }

    private void FallFromPlatform()
    {
        playerMovement.FastFallState();
        playerMovement.TransparentState();
    }

    /// <summary>
    /// Follows the player horizontally (used when AI is on some sort of ground)
    /// </summary>
    private void FollowPlayerOnGround()
    {
        if (relativeSideY >= 4.5f) //si le player est trop haut par rapport à l'IA
            DirectionalDash((int)playerMovement.direction, 1);

        if (distanceX > 2.5f) //si le player est un peu loin de l'IA (en x)
        {
            playerMovement.moveInputx = playerMovement.direction; //aller vers le joueur

            if (distanceX > 6f) //Si très loin, dash vers le joueur
                DirectionalDash((int)playerMovement.direction, 0);
        }
    }

    /// <summary>
    /// Recovers to the terrain (used when AI is outside of the terrain)
    /// </summary>
    private void RecoverToTerrain()
    {
        RaycastHit2D isThereGroundLeft = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector2.left), 100f, 1 << 8);
        RaycastHit2D isThereGroundRight = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector2.right), 100f, 1 << 8);
        RaycastHit2D isThereGroundUnderLeft = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(new Vector2(-1, -1)), 100f, 1 << 8);
        RaycastHit2D isThereGroundUnderRight = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(new Vector2(1, -1)), 100f, 1 << 8);

        if (relativeSideX > 0) //si le joueur est à droite de l'IA
        {
            if (isThereGroundLeft || isThereGroundUnderLeft) //Si le terrain est à gauche de l'IA
            {
                if (isThereGroundRight || isThereGroundUnderRight) //Si le terrain est aussi à droite de l'IA
                    AerialRecovery(1);
                else //S'il n'y a pas de terrain à droite de l'IA
                    AerialRecovery(-1);
            }
            else //Si le terrain est à droite de l'IA
            {
                AerialRecovery(1);
            }
        }
        else //si le joueur est à gauche de l'IA
        {
            if (isThereGroundRight || isThereGroundUnderRight) //Si le terrain est à droite de l'IA
            {
                if (isThereGroundLeft || isThereGroundUnderLeft) //Si le terrain est aussi à gauche de l'IA
                    AerialRecovery(-1);
                else //S'il n'y a pas de terrain à gauche de l'IA
                    AerialRecovery(1);
            }
            else //Si le terrain est à gauche de l'IA
            {
                AerialRecovery(-1);
            }
        }
    }

    /// <summary>
    /// When in the air and next to a ledge, used to get back above terrain
    /// </summary>
    private void AerialRecovery(int x)
    {
        if (playerMovement.nbrJump > 0)
            DirectionalJump(x); //Saut vers la gauche ou la droite
        else if (playerMovement.nbrDash > 0 && !playerMovement.isJumping)
            DirectionalDash(x, 1); //Dash vers le haut-gauche ou haut-droite
        else
            playerMovement.moveInputx = x; //Move vers la gauche ou la droite
    }
}
