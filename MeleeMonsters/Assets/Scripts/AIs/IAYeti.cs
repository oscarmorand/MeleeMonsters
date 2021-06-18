﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAYeti : IAAttacks
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void UseGroundAttacks()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= 0f)
        {
            if (distanceX < 1f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3(0, 1, 2)); //sG / dG / nG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 2)); //sG / nG

                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG); //nG

                if (relativeSideY < 4f && rd.Next(0, 18) == 0) //1 chance sur 18
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS
            }
            if (distanceX < 1.75f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 1)); //sG / dG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sG); //sG
            }
            if (distanceX < 2.25f)
            {
                if (relativeSideY < 1f && rd.Next(0, 10) == 0) //1 chance sur 10
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS
            }
        }
        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 0.5f && distanceX < 1.75f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dG); //dG

            if (relativeSideY < 1f && distanceX < 2.25f && rd.Next(0, 5) == 0) //1 chance sur 5
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS
        }

        if (relativeSideY < 2.5f && distanceX >= 4f && rd.Next(0, 7) == 0) //1 chance sur 7
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sS); //sS
    }

    public override void UseAirAttacks()
    {
        //System.Random rd = new System.Random();

        //if (relativeSideY >= 0f)
        //{
        //    if (distanceX < 1f)
        //    {
        //        if (relativeSideY < 1f)
        //            playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3(0, 1, 2)); //sG / dG / nG

        //        if (relativeSideY < 1.5f)
        //            playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 2)); //sG / nG

        //        if (relativeSideY < 2f)
        //            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG); //nG

        //        if (relativeSideY < 4f && rd.Next(0, 12) == 0) //1 chance sur 12
        //            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS
        //    }
        //    if (distanceX < 1.75f)
        //    {
        //        if (relativeSideY < 1f)
        //            playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 1)); //sG / dG

        //        if (relativeSideY < 1.5f)
        //            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sG); //sG
        //    }
        //    if (distanceX < 2.5f)
        //    {
        //        if (relativeSideY < 1f && rd.Next(0, 5) == 0) //1 chance sur 5
        //            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS
        //    }
        //}
        //if (relativeSideY >= -1f)
        //{
        //    if (relativeSideY < 0.5f && distanceX < 1.75f)
        //        playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dG); //dG

        //    if (relativeSideY < 1f && distanceX < 2.5f && rd.Next(0, 5) == 0) //1 chance sur 5
        //        playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS
        //}

        //if (relativeSideY < 2.5f && distanceX >= 4f && rd.Next(0, 7) == 0) //1 chance sur 7
        //    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sS); //sS
    }

    public override void UseGroundAttacksW()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= 0f)
        {
            if (distanceX < 1f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3(0, 1, 2)); //sG / dG / nG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 2)); //sG / nG

                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG); //nG
            }
            if (distanceX < 1.75f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 1)); //sG / dG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sG); //sG

                if (relativeSideY < 4f && rd.Next(0, 12) == 0) //1 chance sur 12
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nW); //nW
            }
            if (distanceX < 2.5f)
            {
                if (relativeSideY < 1f && rd.Next(0, 8) == 0) //1 chance sur 8
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dW); //dW
            }
        }
        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 0.5f && distanceX < 1.75f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dG); //dG

            if (relativeSideY < 1f && distanceX < 2.5f && rd.Next(0, 5) == 0) //1 chance sur 5
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dW); //dW
        }

        if (relativeSideY < 2.5f && distanceX >= 3f && rd.Next(0, 8) == 0) //1 chance sur 8
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sW); //sW
    }

    public override void UseAirAttacksW()
    {

    }
}
