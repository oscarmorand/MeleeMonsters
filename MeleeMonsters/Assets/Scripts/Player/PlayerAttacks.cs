using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public enum attackType
    {
        sG,
        dG,
        nG,

        sA,
        dA,
        nA,

        sS,
        dS,
        nS,

        sW,
        dW,
        nW
    }

    internal bool normalButton;
    internal bool specialButton;
    internal bool stoppedPressing;
    internal float specialTimeStarted;
    internal float specialTimeFinished;

    private PlayerMovement pM;
    private PlayerScript pS;
    private PhotonView pV;
    private Rigidbody2D rb;
    internal PlayerAnimation pAn;
    private AudioManager aM;

    public bool canAttack = true;

    public MonstersAttacks monstersAttacks;

    public PlayerScript.Monsters monster;
    private PlayerScript playerScript;

    public Attack currentAttack = null;

    public void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        pM = GetComponent<PlayerMovement>();
        pS = GetComponent<PlayerScript>();
        pV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        pAn = GetComponent<PlayerAnimation>();
        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Update()
    {
        bool isPlaying = (playerScript.currentState == PlayerScript.States.Playing);

        if (!isPlaying)
            return;

        if (canAttack && !pS.isHitStun)
        {
            if (normalButton || specialButton)
                PlayerExecuteAttack();
        }
    }

    public void PerformAttack(int attackNbr)
    {
        canAttack = false;

        Attack attack = monstersAttacks.attacks[attackNbr];

        currentAttack = attack;
        pV.RPC("ChangeCurrentAttack", RpcTarget.All, Attack.Serialize(attack));

        monstersAttacks.Attack(attack.name);

        //AttackAnimation(attack);
        pV.RPC("AttackAnimation", RpcTarget.All, attack.anim);
        //AttackSFX(attack);
        pV.RPC("AttackSFX", RpcTarget.All, attack.sound);


        Invoke("EndOfAttack", attack.time);
    }

    public void PlayerExecuteAttack()
    {
        int attackNbr = DetermineAttack();
        PerformAttack(attackNbr);
    }

    public void IAExecuteAttack(attackType choice)
    {
        if (canAttack && !pS.isHitStun)
        {
            PerformAttack((int)choice);
        }
    }


    private void EndOfAttack()
    {
        canAttack = true;
        currentAttack = null;
    }

    public static float CalculateForce(float attackForce, int targetPercentage, float weight, float wrathBonus)
    {
        //return attackForce + (attackForce * targetPercentage/10);
        //return (float)((((targetPercentage*3/20)*(1.4/weight))+attackForce) * wrathBonus);
        return (float)((attackForce*(targetPercentage+20)*wrathBonus) / (weight*20));
    }

    public static float CalculateHitStun(float knockback)
    {
        return knockback / 400;
    }


    public int DetermineAttack()
    {
        int type = DetermineAttackType();
        int direction = DetermineDirection();

        return (type * 3) + direction;
    }

    public int DetermineAttackType()
    {
        if (normalButton)
        {
            if (pM.isGrounded || pM.isOnPlatform) // Weak Attack;
                return 0;
            else // Air Attack;
                return 1;
        }
        else  
        {
            if (pS.isWrath) // Wrath special attack
                return 3;
            else            // Special Attack
                return 2;
        }

    }

    public int DetermineDirection()
    {
        if(pM.moveInputx != 0) // Side Attack
            return 0;
        else
        {
            if(pM.isPressingDown) // Down Attack
                return 1;
            else  // Neutral Attack
                return 2;
        }
    }


    [PunRPC]
    public void AttackSFX(string sound)
    {
        if (sound != "")
            aM.Play(sound);
    }

    [PunRPC]
    public void AttackAnimation(string animationAttack)
    {
        if (animationAttack != "")
            pAn.Attack(animationAttack);
    }

    [PunRPC] 
    public void ChangeCurrentAttack(float[] data)
    {
        currentAttack = Attack.Deserialize(data);
    }
}
