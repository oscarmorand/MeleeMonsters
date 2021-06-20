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

        attacks.Add(new Attack("SideGround", 3, 200, new Vector2(2f, 1f), 0.6f,"Sg","", 0.8f));
        attacks.Add(new Attack("DownGround", 3, 200, new Vector2(1f, 1), 0.6f,"Dg", "", 0.8f));
        attacks.Add(new Attack("NeutralGround", 4, 210,  new Vector2(0, 1), 0.9f,"Ng", "", 0.9f));

        attacks.Add(new Attack("SideAir", 3, 200,  new Vector2(2, 1f), 0.6f, "Sa", "", 0.8f));
        attacks.Add(new Attack("DownAir", 6, 250,  new Vector2(0, -2), 0.9f,"Da", "", 0.8f));
        attacks.Add(new Attack("NeutralAir", 5, 100,  new Vector2(0, 1), 0.6f,"Na", "expansion", 0.9f));

        attacks.Add(new Attack("SideSpecial", 5, 150, new Vector2(0, 2f), 1f, "Ss", "invisibility", 1f));
        attacks.Add(new Attack("DownSpecial", 2, 75,  new Vector2(0, 1f), 0.6f, "Ds", "", 0.2f));
        attacks.Add(new Attack("NeutralSpecial", 6, 450, new Vector2(1f, 1f), 1f, "Ns", "", 1f));

        attacks.Add(new Attack("SideWrath", 8, 280,  new Vector2(0, 2), 1.4f,"Sw", "", 0.8f));
        attacks.Add(new Attack("DownWrath", 5, 150,  new Vector2(1, 1),  0.7f, "Dw", "", 0.2f));
        attacks.Add(new Attack("NeutralWrath", 10, 700, new Vector2(1, 1f), 1.2f, "Nw", "", 0.9f));
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
