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

    internal bool canAttack = true;

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
    }

    public void Update()
    {
        bool isPlaying = (playerScript.currentState == PlayerScript.States.Playing);

        if (!isPlaying)
            return;

        if (canAttack)
        {
            if (normalButton || specialButton)
                PerformAttack();
        }
    }

    public void PerformAttack()
    {
        int attackNbr = DetermineAttack();

        canAttack = false;

        Attack attack = monstersAttacks.attacks[attackNbr];

        monstersAttacks.Attack(attack.name);

        Invoke("EndOfAttack", attack.durationTime);
    }

    //public void BasicAttack(Attack attack)
    //{
    //    LayerMask layerMask = (1<<9) | (1<<11);
    //    Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attack.hitBox.position, attack.size, layerMask);
    //    foreach (Collider2D playerCollider in hitColliders)
    //    {
    //        if (playerCollider != null && playerCollider.transform != transform)
    //        {
    //            if(playerCollider.transform.tag == "Player" || playerCollider.transform.tag == "IA")
    //            {
    //                if (pV.IsMine)
    //                {
    //                    //PlayerScript targetScript = playerCollider.GetComponent<PlayerScript>();
    //                    PhotonView pVTarget = playerCollider.GetComponent<PhotonView>();

    //                    float bonus = 1;
    //                    if (pS.isWrath)
    //                    {
    //                        bonus = 1.25f;
    //                        WrathSustain(attack.damage);
    //                    }
    //                    int newDamage = (int)((float)(attack.damage) * bonus);

    //                    Vector2 direction = attack.direction;
    //                    Vector2 newDirection = new Vector2((direction.x) * pM.direction, (direction.y));
    //                    pVTarget.RPC("Eject", RpcTarget.All, newDirection, attack.ejection, bonus);

    //                    pVTarget.RPC("TakeDamage", RpcTarget.All, newDamage);
    //                }
    //            }
    //        }
    //    }
    //}

    public IEnumerator BasicAttack(Attack attack)
    {
        yield return new WaitForSeconds(attack.activationTime);

        print("je m'active");
        attack.hitbox.SetActive(true);
        currentAttack = attack;

        yield return new WaitForSeconds(attack.durationTime);

        print("je me désactive");
        attack.hitbox.SetActive(false);

        yield return new WaitForSeconds(attack.disabledTime);

        print("ahhh je peux de nouveau attaquer");
        EndOfAttack();
    }



    public static float CalculateForce(float attackForce, int targetPercentage, float weight, float wrathBonus)
    {
        //return attackForce + (attackForce * targetPercentage/10);
        //return (float)((((targetPercentage*3/20)*(1.4/weight))+attackForce) * wrathBonus);
        return (float)((attackForce*(targetPercentage+20)*wrathBonus) / (weight*20));
    }


    public void WrathSustain(int damage)
    {
        float sustain = (float)damage / 20;
        print("sustain de "+sustain+ " secondes!");
        pS.wrathTime += sustain;
    }

    private void EndOfAttack()
    {
        canAttack = true;
        currentAttack = null;
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
            {
                return 0;
            }
            else // Air Attack;
            {
                return 1;
            }
        }
        else  
        {
            if (pS.isWrath) // Wrath special attack
            {
                return 3;
            }
            else            // Special Attack
            {
                return 2;
            }
        }

    }

    public int DetermineDirection()
    {
        if(pM.moveInputx != 0) // Side Attack
        {
            return 0;
        }
        else
        {
            if(pM.isPressingDown) // Down Attack
            {
                return 1;
            }
            else  // Neutral Attack
            {
                return 2;
            }
        }
    }

    public void IAExecuteAttack(attackType choice)
    {
        if (canAttack)
        {
            canAttack = false;

            Attack currentAttack = monstersAttacks.attacks[(int)choice];
            monstersAttacks.Attack(currentAttack.name);

            Invoke("EndOfAttack", currentAttack.durationTime);
        }
    }
}
