using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class MonstersAttacks : MonoBehaviour
{

    public List<Attack> attacks;

    public List<Transform> hitboxesPoints;

    private PhotonView pV;
    private PlayerScript pS;
    internal PlayerMovement pM;
    internal PlayerAttacks pA;

    void Start()
    {
        pV = GetComponent<PhotonView>();
        pS = GetComponent<PlayerScript>();
        pM = GetComponent<PlayerMovement>();
        pA = GetComponent<PlayerAttacks>();

        InstantiateAttacks();
    }

    public abstract void InstantiateAttacks();


    public void Attack(string attackName)
    {
        Invoke(attackName,0);
    }


    public abstract void SideWeak();

    public abstract void NeutralWeak();

    public abstract void DownWeak();

    public abstract void SideSpecial();

    public abstract void NeutralSpecial();

    public abstract void DownSpecial();

    public abstract void SideAir();

    public abstract void NeutralAir();

    public abstract void DownAir();

}
