using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    internal bool normalButton;
    internal bool specialButton;

    private PlayerMovement pM;
    private PlayerScript pS;
    private PhotonView pV;

    internal bool isAttacking = false;
    internal bool canAttack = true;

    //public AvocadoAttacks avocadoAttacks;
    public MonstersAttacks monstersAttacks;

    public PlayerScript.Monsters monster;

    public void Start()
    {
        pM = GetComponent<PlayerMovement>();
        pS = GetComponent<PlayerScript>();
        pV = GetComponent<PhotonView>();
    }

    public void Update()
    {
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

                        Vector2 direction = attack.direction;
                        Vector2 newDirection = new Vector2((direction.x) * pM.direction, (direction.y));
                        pVTarget.RPC("Eject", RpcTarget.All, newDirection, attack.ejection);

                        pVTarget.RPC("TakeDamage", RpcTarget.All, attack.damage);
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
    private void Eject(Vector2 direction, float force)
    {
        float factor = CalculateForce(force, pS.percentage);
        pM.Eject(direction*factor);
    }


    public static float CalculateForce(float attackForce, int targetPercentage)
    {
        return attackForce + (attackForce * targetPercentage/10);
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
                return 2;
            }
        }
        else  // Special Attack
        {
            return 1;
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
