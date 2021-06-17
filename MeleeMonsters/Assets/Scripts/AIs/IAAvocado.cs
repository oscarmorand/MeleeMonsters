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

        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 1f && distanceX < 1.5f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dG); //dG
        }
        if (relativeSideY >= 0f)
        {
            if (distanceX < 1.5f)
            {
                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sG); //sG

                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)rd.Next(0, 2)); //sG / dG
            }
            if (distanceX < 1f)
            {
                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG); //nG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)(rd.Next(0, 3) == 1 ? 7 : (rd.Next(0, 2) == 1 ? 2 : 0))); //sG / nG / dS

                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)(rd.Next(0, 4) == 3 ? 7 : rd.Next(0, 3))); //sG / dG / nG / dS
            }
        }

        if (distanceY < 0.5f && distanceX >= 4f && rd.Next(0, 7) == 1) //1 chance sur 7
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS

        if (distanceY < 1f && distanceX >= 3f && rd.Next(0, 25) == 1) //1 chance sur 25
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sS); //sS
    }

    public override void UseAirAttacks()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= -2f)
        {
            if (relativeSideY < 0f && distanceX < 0.75f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dA); //dA
        }
        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 1f && distanceX < 1.25f && rd.Next(0, 5) == 1)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS (1 chance sur 5)
        }
        if (relativeSideY >= 0f)
        {
            if (distanceX < 1.5f)
            {
                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sA); //sA
            }
            if (distanceX < 1f)
            {
                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)(rd.Next(5, 7) == 6 ? (rd.Next(0,5) == 1 ? 7 : 5) : 5)); //nA / dS (1 chance sur 5)

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)(rd.Next(3, 5) == 4 ? 5 : (rd.Next(3,5) == 4 ? (rd.Next(0, 5) == 1 ? 7 : 3) : 5))); //sA / nA / dS (1 chance sur 5)
            }
        }
        
        if (distanceY < 0.5f && distanceX >= 4f && rd.Next(0, 7) == 1) //1 chance sur 7
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS

        if (distanceY < 1f && distanceX >= 3f && rd.Next(0, 25) == 1) //1 chance sur 25
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sS); //sS
    }

    public override void UseGroundAttacksW()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 1f && distanceX < 1.5f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dG); //dG
        }
        if (relativeSideY >= 0f)
        {
            if (distanceX < 1.5f)
            {
                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sG); //sG

                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)rd.Next(0, 2)); //sG / dG
            }
            if (distanceX < 1f)
            {
                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG); //nG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)(rd.Next(0, 3) == 1 ? 10 : (rd.Next(0, 2) == 1 ? 2 : 0))); //sG / nG / dW

                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)(rd.Next(0, 4) == 3 ? 10 : rd.Next(0, 3))); //sG / dG / nG / dW
            }
        }

        if (distanceY < 2f && distanceX >= 4f && rd.Next(0, 20) == 1) //1 chance sur 20
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nW); //nW

        if (distanceY < 1f && distanceX >= 3f && rd.Next(0, 30) == 1) //1 chance sur 30
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sW); //sW
    }

    public override void UseAirAttacksW()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= -2f)
        {
            if (relativeSideY < 0f && distanceX < 0.75f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dA); //dA
        }
        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 1f && distanceX < 1.25f && rd.Next(0, 3) == 1)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dW); //dW (1 chance sur 3)
        }
        if (relativeSideY >= 0f)
        {
            if (distanceX < 1.5f)
            {
                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sA); //sA
            }
            if (distanceX < 1f)
            {
                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)(rd.Next(5, 7) == 6 ? (rd.Next(0, 5) == 1 ? 10 : 5) : 5)); //nA / dW (1 chance sur 5)

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)(rd.Next(3, 5) == 4 ? 5 : (rd.Next(3, 5) == 4 ? (rd.Next(0, 5) == 1 ? 10 : 3) : 5))); //sA / nA / dW (1 chance sur 5)
            }
        }

        if (distanceY < 5f && distanceX >= 3.5f && rd.Next(0, 30) == 1) //1 chance sur 30
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nW); //nW

        if (distanceY < 1f && distanceX >= 3f && rd.Next(0, 30) == 1) //1 chance sur 30
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sW); //sW
    }
}
