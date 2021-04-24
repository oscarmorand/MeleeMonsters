using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    internal bool normalButton;
    internal bool specialButton;
    internal bool stoppedPressing;
    internal float specialTimeStarted;
    internal float specialTimeFinished;

    private PlayerMovement pM;
    private PlayerScript pS;
    private PhotonView pV;
    private Rigidbody2D rb;

    internal bool isAttacking = false;
    internal bool canAttack = true;

    public MonstersAttacks monstersAttacks;

    public PlayerScript.Monsters monster;
    private PlayerScript playerScript;

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

        if (canAttack && !isAttacking)
        {
            if (normalButton || specialButton)
                PerformAttack();
        }
    }

    public void PerformAttack()
    {
        int attackNbr = DetermineAttack();

        isAttacking = true;
        canAttack = false;

        Attack attack = monstersAttacks.attacks[attackNbr];

        monstersAttacks.Attack(attack.name);

        Invoke("EndOfAttack", attack.time);
    }

    public void BasicAttack(Attack attack)
    {
        LayerMask layerMask = (1<<9) | (1<<11);
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attack.hitBox.position, attack.size, layerMask);
        foreach (Collider2D playerCollider in hitColliders)
        {
            if (playerCollider != null && playerCollider.transform != transform)
            {
                if(playerCollider.transform.tag == "Player" || playerCollider.transform.tag == "IA")
                {
                    if (pV.IsMine)
                    {
                        //PlayerScript targetScript = playerCollider.GetComponent<PlayerScript>();
                        PhotonView pVTarget = playerCollider.GetComponent<PhotonView>();

                        float bonus = 1;
                        if (pS.isWrath)
                        {
                            bonus = 1.25f;
                            WrathSustain(attack.damage);
                        }
                        int newDamage = (int)((float)(attack.damage) * bonus);

                        Vector2 direction = attack.direction;
                        Vector2 newDirection = new Vector2((direction.x) * pM.direction, (direction.y));
                        pVTarget.RPC("Eject", RpcTarget.All, newDirection, attack.ejection, bonus);

                        pVTarget.RPC("TakeDamage", RpcTarget.All, newDamage);
                    }
                }
            }
        }
    }

    [PunRPC]
    private void TakeDamage(int damage)
    {
        pS.TakeDamage(damage);
    }

    [PunRPC]
    private void Eject(Vector2 direction, float force,float bonus)
    {
        float factor = CalculateForce(force, pS.percentage,rb.mass,bonus);
        pM.Eject(direction*factor);
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
        isAttacking = false;
        canAttack = true;
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
                return 2;
            }
            else  // Neutral Attack
            {
                return 1;
            }
        }
    }
}
