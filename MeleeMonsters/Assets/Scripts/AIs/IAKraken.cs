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

        if (distanceY < 0.5f && distanceX >= 4f && rd.Next(0, 7) == 0) //1 chance sur 7
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nS); //nS

        if (distanceY < 1f && distanceX >= 3f && rd.Next(0, 25) == 0) //1 chance sur 25
            playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sS); //sS
    }

    public override void UseAirAttacks()
    {
        //faire une side air
        //if ((distanceX > 0f && distanceX <= 1.5f) && (relativeSideY > -1f && relativeSideY <= 1f))
        //    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sA); 

        //faire une down air
        //if (distanceX < 0.75f)
        //{
        //    if (relativeSideY < -0.5f && relativeSideY >= -2f)
        //        playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dA); 

        //faire une neutral air
        //    if (relativeSideY > 0.5f && relativeSideY <= 2f)
        //        playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nA); 
        //}
    }

    public override void UseGroundAttacksW()
    {

    }

    public override void UseAirAttacksW()
    {

    }
}
