using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public enum attackType
    {
        sG = 0,
        dG = 1,
        nG = 2,

        sA = 3,
        dA = 4,
        nA = 5,

        sS = 6,
        dS = 7,
        nS = 8,

        sW = 9,
        dW = 10,
        nW = 11,
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

    public static float CalculateForce(float attackForce, int targetPercentage, float weight, float wrathBonus, int attackDamage)
    {
        //return attackForce + (attackForce * targetPercentage/10);
        //return (float)((((targetPercentage*3/20)*(1.4/weight))+attackForce) * wrathBonus);
        return (((( targetPercentage/10 + targetPercentage *  (attackDamage/20)) * weight * 1.4f) + 18) + attackForce);
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

    public void PlayAttackAnimation(string animationName) //utilisée que par le Kraken dans son downSpecial
    {
        pV.RPC("AttackAnimation", RpcTarget.All, animationName);
    }
}
