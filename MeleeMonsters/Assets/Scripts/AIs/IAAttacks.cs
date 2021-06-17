using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class IAAttacks : MonoBehaviour
{
    internal PlayerMovement playerMovement;
    internal GameObject player;
    internal Transform playerTrans;

    internal PlayerAttacks playerAttacks;
    internal PlayerScript playerScript;
    internal IA ia;

    public float distanceX;
    public float distanceY;
    public float relativeSideX;
    public float relativeSideY;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.dashInputy = 0;

        playerAttacks = GetComponent<PlayerAttacks>();
        playerScript = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    protected virtual void Update()
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
        }
    }

    abstract public void UseGroundAttacks();

    abstract public void UseAirAttacks();

    abstract public void UseGroundAttacksW();

    abstract public void UseAirAttacksW();
}
