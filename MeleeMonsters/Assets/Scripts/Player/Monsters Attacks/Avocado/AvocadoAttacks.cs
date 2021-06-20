using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoAttacks : MonstersAttacks, IPunObservable
{

    public GameObject bulletPrefab;
    public GameObject bulletWrathPrefab;

    public SpriteRenderer noyau;

    public override void InstantiateAttacks()
    {
        attacks = new List<Attack>();

        attacks.Add(new Attack("SideGround", 4, 190, new Vector2(2, 1), 0.5f,"Sg", "wooshe4", 0.5f));
        attacks.Add(new Attack("DownGround", 5, 190, new Vector2(1, 1), 0.7f,"Dg", "wooshe7", 0.9f));
        attacks.Add(new Attack("NeutralGround", 3, 150, new Vector2(0, 2), 0.4f,"Ng","wooshe10", 0.8f));


        attacks.Add(new Attack("SideAir", 4, 190, new Vector2(2f, 1),  0.45f,"Sa","wooshe4", 0.5f));
        attacks.Add(new Attack("DownAir", 6, 190, new Vector2(0, -2), 0.3f,"Da", "wooshe7", 0.9f));
        attacks.Add(new Attack("NeutralAir", 3, 150, new Vector2(0, 2), 0.3f,"Na", "wooshe10", 0.8f));


        attacks.Add(new Attack("SideSpecial", 5, 400, new Vector2(0, 1f), 1f, "Ss","wooshb4", 1f));
        attacks.Add(new Attack("DownSpecial", 7, 500, new Vector2(0, 1), 1.5f, "Ds", "", 1f));
        attacks.Add(new Attack("NeutralSpecial", 2, 50, new Vector2(0, 1), 0.5f, "Ns","bouchon champagne", 1f));


        attacks.Add(new Attack("SideWrath", 10, 600, new Vector2(1, 0), 2f, "Sw", "wooshb4", 1f));
        attacks.Add(new Attack("DownWrath", 9, 750, new Vector2(0, 1), 2f, "Dw","", 1f));
        attacks.Add(new Attack("NeutralWrath", 5, 75, new Vector2(0, 1), 0.8f,"Nw", "bouchon champagne", 1f));

    }

    //Basic attacks
    public override void SideGround() { }
    public override void DownGround() { }
    public override void NeutralGround() { }
    public override void SideAir() { }
    public override void DownAir() { }
    public override void NeutralAir() { }



    public override void SideSpecial()
    {
        pM.SetDashAttackState(0f, 0.4f, 15f, new Vector2(1, 0));
    }


    public override void DownSpecial(){}

    public override void NeutralSpecial()
    {
        print("je fais une neutralspecial d'avocat hannnn");
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, hitboxesPoints[6].position, new Quaternion());
        bullet.GetComponent<AvocadoBullet>().Throw(pM.direction, gameObject);
        NoyauDisable();
    }

  

    public override void SideWrath()
    {
        pM.SetDashAttackState(0f, 0.2f, 30f, new Vector2(1, 0));
    }

    public override void DownWrath(){}

    public override void NeutralWrath()
    {
        print("je fais une neutralspecial d'avocat hannnn");
        GameObject bullet = PhotonNetwork.Instantiate(bulletWrathPrefab.name, hitboxesPoints[6].position, new Quaternion());
        bullet.GetComponent<AvocadoWrathBullet>().Throw(pM.direction, gameObject);
        NoyauDisable();
    }


    public override void FastFallAttackCallback() { }



    public void NoyauDisable()
    {
        noyau.enabled = false;
        Invoke("NoyauEnable", 0.8f);
    }

    public void NoyauEnable()
    {
        noyau.enabled = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(noyau.enabled);
        }
        else
        {
            noyau.enabled = (bool)stream.ReceiveNext();
        }
    }

    /*
    /// <summary>
    /// Joue un son de couteau / sabre au hasard
    /// </summary>
    private void PlayRandomKnifeSound()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            aM.Play("slash sabre");
        if (rand == 1)
            aM.Play("slash couteau 1");
        if (rand == 2)
            aM.Play("slash couteau 2");
    }
    */
}
