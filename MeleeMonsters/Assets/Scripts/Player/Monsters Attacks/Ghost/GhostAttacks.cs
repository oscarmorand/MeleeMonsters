using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GhostAttacks : MonstersAttacks, IPunObservable
{
    public GameObject batPrefab;
    public GameObject wrathBatPrefab;

    public override void InstantiateAttacks()
    {
        attacks = new List<Attack>();

        attacks.Add(new Attack("SideGround", 3, 300, new Vector2(1, 0.1f), 0.5f,"Sg","", 1f));
        attacks.Add(new Attack("DownGround", 5, 400, new Vector2(0, 1), 0.5f,"Dg", "", 1f));
        attacks.Add(new Attack("NeutralGround", 4, 150,  new Vector2(0, 1), 0.5f,"Ng", "", 1f));

        attacks.Add(new Attack("SideAir", 5, 400,  new Vector2(1, 0), 0.5f, "Sa", "", 1f));
        attacks.Add(new Attack("DownAir", 6, 200,  new Vector2(0, 1), 0.5f,"Da", "", 1f));
        attacks.Add(new Attack("NeutralAir", 2, 100,  new Vector2(0, 1), 0.5f,"Na", "", 1f));

        attacks.Add(new Attack("SideSpecial", 5, 100, new Vector2(0, 1), 1f, "Ss", "", 1f));
        attacks.Add(new Attack("DownSpecial", 6, 100,  new Vector2(0, 1), 0.6f, "Ds", "", 1f));
        attacks.Add(new Attack("NeutralSpecial", 2, 500, new Vector2(1, 0.1f), 0.6f, "Ns", "", 1f));

        attacks.Add(new Attack("SideWrath", 8, 250,  new Vector2(1, 0), 1.4f,"Sw", "", 1f));
        attacks.Add(new Attack("DownWrath", 10, 200,  new Vector2(0, 1),  1f, "Dw", "", 1f));
        attacks.Add(new Attack("NeutralWrath", 3, 700, new Vector2(1, 0.1f), 0.8f, "Nw", "", 1f));
    }


    //Basic attacks
    public override void SideGround(){}
    public override void DownGround(){}
    public override void NeutralGround(){}
    public override void SideAir(){}
    public override void DownAir(){}
    public override void NeutralAir(){}




    public override void SideSpecial()
    {
        pM.SetDashAttackState(0.2f, 0.08f, 50f, new Vector2(1, 0));
    }

    public override void DownSpecial()
    {
        print("je fais une downSpecial de fantome wouaaaa");
        GameObject bat = PhotonNetwork.Instantiate(batPrefab.name, hitboxesPoints[0].position, new Quaternion());
        bat.GetComponent<GhostBat>().Throw(pM.direction, gameObject);
    }

    public override void NeutralSpecial(){}



    public override void SideWrath()
    {
        pM.SetDashAttackState(0.25f, 0.18f, 32f, new Vector2(1, 0));
    }

    public override void DownWrath()
    {
        GameObject wrathBat = PhotonNetwork.Instantiate(wrathBatPrefab.name, hitboxesPoints[0].position, new Quaternion());
        wrathBat.GetComponent<GhostWrathBat>().Throw(pM.direction, gameObject);
    }

    public override void NeutralWrath()
    {

    }

    public override void FastFallAttackCallback(){}


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
