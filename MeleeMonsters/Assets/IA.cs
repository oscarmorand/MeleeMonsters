using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IA : MonoBehaviour
{
    PlayerMovement playerMovement;
    GameObject player;
    Transform playerTrans;
    float distancex;
    float distancey;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) 
        {
            gameObject.tag = "Untagged";
            player = GameObject.FindGameObjectWithTag("Player");
            playerTrans = player.transform;
        } 
        else
        {
            distancex = Math.Abs(gameObject.transform.position.x - playerTrans.position.x);
            distancey = Math.Abs(gameObject.transform.position.y - playerTrans.position.y);

            
        }
    }
}
