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

        if (relativeSideY >= -1f)
        {
            if (relativeSideY < 0.5f && distanceX < 1.75f)
                playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dG); //dG
        }
        if (relativeSideY >= 0f)
        {
            if (distanceX < 1.75f)
            {
                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sG); //sG

                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 1)); //sG / dG
            }
            if (distanceX < 0.75f)
            {
                if (relativeSideY < 2f)
                    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG); //nG

                if (relativeSideY < 1.5f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random2(0, 2)); //sG / nG

                if (relativeSideY < 1f)
                    playerAttacks.IAExecuteAttack((PlayerAttacks.attackType)Random3(0, 1, 2)); //sG / dG / nG
            }
        }

        if (distanceY < 0.5f && distanceX >= 4f && rd.Next(0, 7) == 0) //1 chance sur 7
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS

        if (distanceY < 1f && distanceX >= 3f && rd.Next(0, 25) == 0) //1 chance sur 25
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sS); //sS
    }

    public override void UseAirAttacks()
    {
        
    }

    public override void UseGroundAttacksW()
    {

    }

    public override void UseAirAttacksW()
    {

    }
}
