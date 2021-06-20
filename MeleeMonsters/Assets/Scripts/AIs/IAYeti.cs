using System.Collections;
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

        if (relativeSideY >= -0.5f)
        {
            if (distanceX < 1f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3(0, 1, 2)); //sG / dG / nG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 2)); //sG / nG

                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG); //nG

                if (relativeSideY < 3f && rd.Next(0, 30) == 0) //1 chance sur 30
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS
            }
            if (distanceX < 1.75f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 1)); //sG / dG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sG); //sG
            }
        }
        if (relativeSideY >= 0.5f && relativeSideY < 1f && distanceX < 2.5f && rd.Next(0, 8) == 0) //1 chance sur 8
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS
        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 0.5f && distanceX < 1.75f)
                playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(1, 7, 6)); //dG / dS (1 chance sur 6)

            if (relativeSideY < 1f && distanceX < 2.25f && rd.Next(0, 6) == 0) //1 chance sur 6
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS
        }

        if (relativeSideY < 2.5f && distanceX >= 4f && rd.Next(0, 7) == 0) //1 chance sur 7
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sS); //sS
    }

    public override void UseAirAttacks()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= 0f)
        {
            if (relativeSideY < 1.5f && distanceX < 1.25f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sA); //sA

            if (relativeSideY < 3.5f && distanceX < 0.75f && rd.Next(0, 40) == 0) //1 chance sur 40
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS

            if (relativeSideY < 4.5f && distanceX >= 3f && rd.Next(0, 10) == 0) //1 chance sur 10
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sS); //sS
        }

        if (relativeSideY >= -0.75f && relativeSideY < 0.5f && distanceX < 0.5f)
            playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(4, 5)); //dA / nA

        if (distanceY < 0.75f && distanceX < 1.75f)
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nA); //nA

        if (relativeSideY >= -1.5f && relativeSideY < 0.5f && distanceX < 0.5f)
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dA); //dA

        if (relativeSideY >= -0.5f && relativeSideY < 1f && distanceX < 2f && rd.Next(0, 10) == 0) //1 chance sur 10
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS
    }

    public override void UseGroundAttacksW()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= -0.5f)
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

                if (relativeSideY < 4f && rd.Next(0, 25) == 0) //1 chance sur 25
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nW); //nW
            }
        }
        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 0.5f && distanceX < 1.75f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dG); //dG

            if (relativeSideY < 1f && distanceX < 2.25f && rd.Next(0, 5) == 0) //1 chance sur 5
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dW); //dW
        }

        if (relativeSideY < 2.5f && distanceX >= 3f && rd.Next(0, 8) == 0) //1 chance sur 8
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sW); //sW
    }

    public override void UseAirAttacksW()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= 0f)
        {
            if (relativeSideY < 1.5f && distanceX < 1.25f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sA); //sA

            if (relativeSideY < 3.5f && distanceX < 0.5f && rd.Next(0, 30) == 0) //1 chance sur 30
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nW); //nW
        }

        if (distanceY < 0.75f && distanceX < 1.75f)
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nA); //nA

        if (relativeSideY >= -0.5f && relativeSideY < 0.5f && distanceX < 0.5f)
            playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(4, 10, 3)); //dA / dW (1 chance sur 3)

        if (relativeSideY >= -1.5f && relativeSideY < 0.5f && distanceX < 0.5f)
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dA); //dA

        if (relativeSideY < 2f && distanceX >= 4f && rd.Next(0, 8) == 0) //1 chance sur 8
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sW); //sW

        if (relativeSideY < -1.5f && distanceX > 2f && rd.Next(0, 30) == 0) //1 chance sur 30
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dW); //dW
    }
}
