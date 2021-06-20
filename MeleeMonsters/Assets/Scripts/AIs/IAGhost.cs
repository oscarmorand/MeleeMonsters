using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAGhost : IAAttacks
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
            if (distanceX < 1.25f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3(0, 1, 2)); //sG / dG / nG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3Luck(0, 2, 8, 3)); //sG / nG / nS (1 chance sur 3)

                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(2, 8, 3)); //nG / nS (1 chance sur 3)
            }

            if (distanceX < 2.5f && relativeSideY < 2f && rd.Next(0, 8) == 0) //1 chance sur 8
                playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(8, 6, 15)); //nS / sS (1 chance sur 15)

            if (distanceX < 4f && relativeSideY < 1.5f && rd.Next(0, 7) == 0) //1 chance sur 7
                playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(6, 7, 5)); //sS / dS (1 chance sur 5)
        }

        if (distanceY < 1.5f && distanceX >= 2.5f && rd.Next(0, 30) == 0) //1 chance sur 30
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS
    }

    public override void UseAirAttacks()
    {
        System.Random rd = new System.Random();

        if (relativeSideY < 1.5f)
        {
            if (relativeSideY >= 0f)
            {
                if (distanceX < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3Luck(3, 5, 8, 5)); //sA / nA / nS (1 chance sur 5)

                if (distanceX < 2f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(5, 8, 3)); //nA / nS (1 chance sur 3) 
            }

            if (relativeSideY >= -0.5f && distanceX < 4f && rd.Next(0, 7) == 0) //1 chance sur 7
                playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(6, 7, 5)); //sS / dS (1 chance sur 5)

            if (relativeSideY >= -1.5f)
            {
                if (relativeSideY < -0.5f && distanceX < 0.75f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(4, 5)); //dA / nA

                if (distanceX < 1.25f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(3, 5)); //sA / nA

                if (distanceX >= 2.5f && rd.Next(0, 30) == 0) //1 chance sur 30
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dS); //dS
            }
        }

        if (relativeSideY >= -2.25f && relativeSideY < -0.5f && distanceX < 0.75f)
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dA); //dA

        if (distanceX < 2.5f && relativeSideY > -0.5f && relativeSideY < 2f && rd.Next(0, 20) == 0) //1 chance sur 20
            playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(8, 6, 3)); //nS / sS (1 chance sur 3)
    }

    public override void UseGroundAttacksW()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= -0.5f)
        {
            if (distanceX < 1.5f)
            {
                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3(0, 1, 2)); //sG / dG / nG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3Luck(0, 2, 11, 3)); //sG / nG / nW (1 chance sur 3)

                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(2, 11, 3)); //nG / nW (1 chance sur 3)
            }

            if (distanceX < 2.5f && relativeSideY < 2f && rd.Next(0, 15) == 0) //1 chance sur 15
                playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(11, 9, 5)); //nW / sW (1 chance sur 5)

            if (distanceX < 5f && relativeSideY < 1.5f && rd.Next(0, 25) == 0) //1 chance sur 25
                playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(9, 10, 5)); //sW / dW (1 chance sur 5)
        }

        if (distanceY < 1.5f && distanceX >= 2.5f && rd.Next(0, 30) == 0) //1 chance sur 30
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dW); //dW
    }

    public override void UseAirAttacksW()
    {
        System.Random rd = new System.Random();

        if (relativeSideY < 1.5f)
        {
            if (relativeSideY >= 0f)
            {
                if (distanceX < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3Luck(3, 5, 11, 5)); //sA / nA / nW (1 chance sur 5)

                if (distanceX < 2.5f && rd.Next(0, 5) == 0)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nW); //nW (1 chance sur 5) 
            }

            if (relativeSideY >= -1f && distanceX < 4f && rd.Next(0, 7) == 0) //1 chance sur 7
                playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(9, 10, 5)); //sW / dW (1 chance sur 5)

            if (relativeSideY >= -1.5f)
            {
                if (relativeSideY < -0.5f && distanceX < 0.75f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(4, 5)); //dA / nA

                if (distanceX < 1.25f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(3, 5)); //sA / nA

                if (distanceX >= 2.5f && rd.Next(0, 30) == 0) //1 chance sur 30
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dW); //dW
            }
        }

        if (relativeSideY >= -2.25f && relativeSideY < -0.5f && distanceX < 0.75f)
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dA); //dA

        if (distanceX < 2.5f && relativeSideY > -0.5f && relativeSideY < 2f && rd.Next(0, 20) == 0) //1 chance sur 20
            playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2Luck(11, 9, 3)); //nW / sW (1 chance sur 3)
    }
}
