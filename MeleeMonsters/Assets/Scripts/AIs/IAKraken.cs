using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAKraken : IAAttacks
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
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3Luck(0, 2, 1, 3)); //sG / dG (1 chance sur 3) / nG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 2)); //sG / nG

                if (relativeSideY < 2.25f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG); //nG
            }
            if (distanceX < 1.75f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 1)); //sG / dG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sG); //sG
            }
            if (distanceX >= 2.5f)
            {
                if (relativeSideY < 1.5f && distanceX < 7.5f && rd.Next(0, 30) == 0) //1 chance sur 30
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(6, 7, 5)); //sS / dS (1 chance sur 5)

                if (relativeSideY < 3f && rd.Next(0, 30) == 0) //1 chance sur 30
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS
            }
        }

        //if (distanceY < 1.5f && distanceX >= 2.5f && rd.Next(0, 7) == 0)
        //    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS (ne marche pas)
    }

    public override void UseAirAttacks()
    {
        System.Random rd = new System.Random();

        if (distanceY < 1.5f)
        {
            if (distanceX >= 0.25f && distanceX < 1.25f)
                playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(3, 6, 15)); //sA / sS (1 chance sur 15)

            if (distanceX >= 2.5f && rd.Next(0, 15) == 0) //1 chance sur 15
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sS); //sS
        }
        
        if (distanceY < 1.5f && distanceX >= 0.25f && distanceX < 1.25f)
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sA); //sA

        if (relativeSideY >= -0.5f && relativeSideY < 1.5f && distanceX < 0.75f)
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nA); //nA

        if (relativeSideY < 2f)
        {
            if (distanceX < 0.75f && rd.Next(0, 5) == 0) //1 chance sur 5
                playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(4, 7, 10)); //dA / dS (1 chance sur 10)

            if (rd.Next(0, 30) == 0) //1 chance sur 30
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS
        }

        //if (distanceY < 1.5f && distanceX >= 2.5f && rd.Next(0, 7) == 0)
        //    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS (ne marche pas)
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

                if (relativeSideY < 2.25f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG); //nG
            }
            if (distanceX < 1.75f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3Luck(0, 1, 10, 5)); //sG / dG / dW (1 chance sur 5)

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(0, 10, 5)); //sG / dW (1 chance sur 5)

                if (relativeSideY < 2.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dW); //dW
            }
            if (distanceX >= 2.5f)
            {
                if (relativeSideY < 1f && rd.Next(0, 20) == 0) //1 chance sur 20
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(11, 9, 10)); //nW / sW (1 chance sur 10)

                if (relativeSideY < 1.5f && rd.Next(0, 20) == 0) //1 chance sur 20
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sW); //sW

            }
        }
    }

    public override void UseAirAttacksW()
    {
        System.Random rd = new System.Random();

        if (distanceY < 1.5f && distanceX >= 0.25f && distanceX < 1.25f)
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sA); //sA

        if (relativeSideY >= -0.5f)
        {
            if (relativeSideY < 1.5f && distanceX >= 0.5f && distanceX < 0.75f)
                playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(5, 10, 30)); //nA / dW (1 chance sur 30)

            if (relativeSideY < 3f && distanceX >= 0.5f && distanceX < 2.5f && rd.Next(0, 30) == 0) //1 chance sur 30
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dW); //dW

            if (relativeSideY < 1.5f && distanceX < 0.75f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nA); //nA
        }

        if (relativeSideY < 0f && distanceX > 2.5f && rd.Next(0, 30) == 0) //1 chance sur 30
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sW); //sW

        if (relativeSideY < 2f && distanceX < 0.75f && rd.Next(0, 5) == 0) //1 chance sur 5
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dA); //dA

        //if (distanceY < 1.5f && distanceX >= 2.5f && rd.Next(0, 7) == 0)
        //    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nW); //nW (ne marche pas)
    }
}
