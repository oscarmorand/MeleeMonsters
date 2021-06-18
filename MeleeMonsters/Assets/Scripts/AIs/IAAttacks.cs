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
        if (playerScript.currentState == PlayerScript.States.Frozen)
            return;

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


    //prob == 1 => 100%
    public int Random2(int a, int b)
    {
        System.Random rd = new System.Random();
        return rd.Next(0, 2) == 0 ? a : b;
    }

    public int Random2Luck(int a, int b, int probb)
    {
        System.Random rd = new System.Random();
        if (rd.Next(0, 2) == 0)
            return a;
        else
            return rd.Next(0, probb) == 0 ? b : a;
    }

    public int Random3(int a, int b, int c)
    {
        System.Random rd = new System.Random();
        int rd3 = rd.Next(0, 3);
        return rd3 == 0 ? a : (rd3 == 1 ? b : c);
    }

    public int Random3Luck(int a, int b, int c, int probc)
    {
        System.Random rd = new System.Random();
        int rd3 = rd.Next(0, 3);

        if (rd3 == 0)
            return a;
        if (rd3 == 1)
            return b;

        if (rd3 == 2 && rd.Next(0, probc) == 0)
            return c;
        else
            return rd.Next(0, 2) == 0 ? a : b;
    }

    public int Random3Luck2(int a, int b, int c, int probb, int probc)
    {
        System.Random rd = new System.Random();
        int rd3 = rd.Next(0, 3);

        if (rd3 == 0)
            return a;

        if (rd3 == 1 && rd.Next(0, probb) == 0)
            return b;

        if (rd3 == 2 && rd.Next(0, probc) == 0)
            return c;

        return a;
    }

    public int Random4(int a, int b, int c, int d)
    {
        System.Random rd = new System.Random();
        int rd4 = rd.Next(0, 4);
        return rd4 == 0 ? a : (rd4 == 1 ? b : (rd4 == 2 ? c : d));
    }

    public int Random4Luck(int a, int b, int c, int d, int probd)
    {
        System.Random rd = new System.Random();
        int rd4 = rd.Next(0, 4);

        int rd3 = rd.Next(0, 3);

        if (rd4 == 0)
            return a;
        if (rd4 == 1)
            return b;
        if (rd4 == 2)
            return c;

        if (rd4 == 3 && rd.Next(0, probd) == 0)
            return d;
        else
            return rd3 == 0 ? a : (rd3 == 1 ? b : c);
    }

    public int Random4Luck2(int a, int b, int c, int d, int probc, int probd)
    {
        System.Random rd = new System.Random();
        int rd4 = rd.Next(0, 4);

        int rd3 = rd.Next(0, 3);

        if (rd4 == 0)
            return a;
        if (rd4 == 1)
            return b;

        if (rd4 == 2 && rd.Next(0, probc) == 0)
            return c;

        if (rd4 == 3 && rd.Next(0, probd) == 0)
            return d;

        return rd.Next(0, 2) == 0 ? a : b;
    }
}
