using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class MonstersAttacks : MonoBehaviour
{

    public List<Attack> attacks;

    public List<Transform> hitboxesPoints;

    public List<GameObject> hitboxes;

    internal PhotonView pV;
    private PlayerScript pS;
    internal PlayerMovement pM;
    internal PlayerAttacks pA;

    internal GameObject aMGameObject;
    internal AudioManager aM;

    void Start()
    {
        pV = GetComponent<PhotonView>();
        pS = GetComponent<PlayerScript>();
        pM = GetComponent<PlayerMovement>();
        pA = GetComponent<PlayerAttacks>();
        aMGameObject = GameObject.Find("AudioManager");
        aM = aMGameObject.GetComponent<AudioManager>();

        InstantiateAttacks();
        AddObservable();
    }

    public abstract void InstantiateAttacks();


    public void Attack(string attackName)
    {
        Invoke(attackName,0);
    }


    public abstract void SideGround();

    public abstract void NeutralGround();

    public abstract void DownGround();

    public abstract void SideSpecial();

    public abstract void NeutralSpecial();

    public abstract void DownSpecial();

    public abstract void SideAir();

    public abstract void NeutralAir();

    public abstract void DownAir();

    public abstract void SideWrath();

    public abstract void NeutralWrath();

    public abstract void DownWrath();


    public abstract void FastFallAttackCallback();


    private void AddObservable()
    {
        if (!pV.ObservedComponents.Contains(this))
        {
            pV.ObservedComponents.Add(this);
        }
    }
}
