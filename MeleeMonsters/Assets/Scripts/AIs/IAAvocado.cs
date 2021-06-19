using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IAAvocado : IAAttacks
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

        if (relativeSideY >= -0.5f)
        {
            if (distanceX < 1.5f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random4(0, 1, 2, 7)); //sG / dG / nG / dS

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3(0, 2, 7)); //sG / nG / dS

                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG); //nG
            }
            if (distanceX < 2f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 1)); //sG / dG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sG); //sG
            }
        }
        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 1f && distanceX < 1.5f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dG); //dG
        }

        if (distanceY < 0.5f && distanceX >= 4f && rd.Next(0, 7) == 0) //1 chance sur 7
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS

        if (distanceY < 1f && distanceX >= 3f && rd.Next(0, 25) == 0) //1 chance sur 25
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sS); //sS
    }

    public override void UseAirAttacks()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= 0f)
        {
            if (distanceX < 1f)
            {
                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3Luck(3, 5, 7, 10)); //sA / nA / dS (1 chance sur 10)

                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(5, 7, 10)); //nA / dS (1 chance sur 10)
            }
            if (distanceX < 1.5f)
            {
                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sA); //sA
            }
        }
        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 1f && distanceX < 1.25f && rd.Next(0, 10) == 0)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS (1 chance sur 10)
        }
        if (relativeSideY >= -2f)
        {
            if (relativeSideY < 0f && distanceX < 0.75f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dA); //dA
        }

        if (distanceY < 0.5f && distanceX >= 4f && rd.Next(0, 7) == 0) //1 chance sur 7
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS

        if (distanceY < 1f && distanceX >= 3f && rd.Next(0, 25) == 0) //1 chance sur 25
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sS); //sS
    }

    public override void UseGroundAttacksW()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= -0.5f)
        {
            if (distanceX < 1f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random4(0, 1, 2, 10)); //sG / dG / nG / dW

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3(0, 2, 10)); //sG / nG / dW

                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG); //nG
            }
            if (distanceX < 1.5f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 1)); //sG / dG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sG); //sG
            }
        }
        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 1f && distanceX < 1.5f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dG); //dG
        }


        if (distanceY < 2f && distanceX >= 4f && rd.Next(0, 20) == 0) //1 chance sur 20
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nW); //nW

        if (distanceY < 1f && distanceX >= 3f && rd.Next(0, 30) == 0) //1 chance sur 30
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sW); //sW
    }

    public override void UseAirAttacksW()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= 0f)
        {
            if (distanceX < 1f)
            {
                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3Luck(3, 5, 10, 3)); //sA / nA / dW (1 chance sur 3)

                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(5, 10, 3)); //nA / dW (1 chance sur 3)
                
            }
            if (distanceX < 1.5f)
            {
                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sA); //sA
            }
        }
        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 1f && distanceX < 1.25f && rd.Next(0, 3) == 0)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dW); //dW (1 chance sur 3)
        }
        if (relativeSideY >= -2f)
        {
            if (relativeSideY < 0f && distanceX < 0.75f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dA); //dA
        }

        if (distanceY < 5f && distanceX >= 3.5f && rd.Next(0, 30) == 0) //1 chance sur 30
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nW); //nW

        if (distanceY < 1f && distanceX >= 3f && rd.Next(0, 30) == 0) //1 chance sur 30
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sW); //sW
    }
}
