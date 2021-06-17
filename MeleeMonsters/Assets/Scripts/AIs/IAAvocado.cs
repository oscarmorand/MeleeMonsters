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
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)(rd.Next(0, 2) == 1 ? 2 : 0)); //sG / nG

                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)rd.Next(0, 3)); //sG / dG / nG
            }
        }
        if (distanceY < 0.5f && distanceX >= 4f && rd.Next(0, 5) == 1) //1 chance sur 5
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS

    }

    public override void UseAirAttacks()
    {
        System.Random rd = new System.Random();

        if (relativeSideY >= -2f)
        {
            if (relativeSideY < 0f && distanceX < 0.75f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dA); //dA
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
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nA); //nA

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)(rd.Next(3, 5) == 4 ? 5 : 3)); //sA / nA
            }
        }
    }

    //wrathground

    //wrathair
}
