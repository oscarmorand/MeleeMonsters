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

        attacks.Add(new Attack("SideGround", 4, 150, new Vector2(1, 0.1f), 0.3f,"Sg", "slash sabre"));
        attacks.Add(new Attack("DownGround", 5, 150, new Vector2(0, 1), 0.3f,"Dg", "slash sabre"));
        attacks.Add(new Attack("NeutralGround", 3, 100, new Vector2(0, 1), 0.3f,"Ng","slash sabre"));


        attacks.Add(new Attack("SideAir", 4, 350, new Vector2(1, 0),  0.3f,"Sa","slash sabre"));
        attacks.Add(new Attack("DownAir", 6, 250, new Vector2(0, -1), 0.3f,"Da","slash sabre"));
        attacks.Add(new Attack("NeutralAir", 3, 300, new Vector2(0, 1), 0.3f,"Na","slash sabre"));


        attacks.Add(new Attack("SideSpecial", 7, 500, new Vector2(1, 0.2f), 0.8f, "Ss",""));
        attacks.Add(new Attack("DownSpecial", 6, 400, new Vector2(0, 1), 1f, "Ds",""));
        attacks.Add(new Attack("NeutralSpecial", 2, 50, new Vector2(0, 1), 0.5f, "Ns","bouchon champagne"));


        attacks.Add(new Attack("SideWrath", 10, 750, new Vector2(1, 0), 1f, "Sw",""));
        attacks.Add(new Attack("DownWrath", 9, 500, new Vector2(0, 1), 1f, "Dw",""));
        attacks.Add(new Attack("NeutralWrath", 4, 75, new Vector2(0, 1), 0.7f,"Nw", "bouchon champagne"));

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
