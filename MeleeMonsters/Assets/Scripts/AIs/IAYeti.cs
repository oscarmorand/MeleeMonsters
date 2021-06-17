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
        //faire une side ground
        //if (distanceX < 1.5f && (relativeSideY > 0f && relativeSideY <= 1.25f))
        //    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.sG); 

        //faire une down ground
        //if (distanceX < 1.35f && (relativeSideY < 0.75f && relativeSideY >= -1f))
        //    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.dG); 

        //faire une neutral ground
        //if (distanceX < 0.75f && relativeSideY > 0.5f && relativeSideY <= 2f)
        //    playerAttacks.IAExecuteAttack(PlayerAttacks.attackType.nG); 
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

    public void UseSpecialAttacks()
    {

    }

    public void UseWrathAttacks()
    {

    }
}
