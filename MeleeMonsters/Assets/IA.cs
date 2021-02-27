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

            //flip pour regarder dans la direction du player
            if (relativeSideX < 0 && playerMovement.facingRight)
                playerMovement.Flip();
            if (relativeSideX > 0 && !playerMovement.facingRight)
                playerMovement.Flip();

            //dash si trop loin du player
            if (distanceX > 4f)
                playerMovement.DashState();

            if (relativeSideY > 2f)
            {
                if (playerMovement.nbrJump > 0)
                    playerMovement.isJumping = true;
                else if (playerMovement.nbrDash > 0)
                {
                    playerMovement.dashInputy = 1;
                    playerMovement.DashState();
                }
                else
                    playerMovement.FastFallState();
            }
        }
    }
}
