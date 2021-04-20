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

    public AvocadoAttacks avocadoAttacks;

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

        Attack attack;
        switch (monster)
        {
            case (PlayerScript.Monsters.Avocado):
                attack = avocadoAttacks.attacks[attackNbr];
                break;
            default:
                attack = null;
                break;
        }

        print(pS.nickName+" performe une "+attack.name);

        LayerMask layerMask = 9 | 10;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attack.hitBox.position,attack.size, layerMask);
        foreach (Collider2D playerCollider in hitColliders)
        {
            if(playerCollider != null && playerCollider.transform != transform)
            {
                if(pV.IsMine)
                {
                    PlayerScript targetScript = playerCollider.GetComponent<PlayerScript>();
                    PhotonView pVTarget = playerCollider.GetComponent<PhotonView>();

                    Vector2 direction = attack.direction;
                    float force = CalculateForce(attack.ejection, targetScript.percentage);
                    Vector2 ejectionVector = new Vector2((direction.x) * force * pM.direction, (direction.y) * force);
                    pVTarget.RPC("Eject", RpcTarget.All, ejectionVector);
                        
                    pVTarget.RPC("TakeDamage", RpcTarget.All, attack.damage);
                }
 
            }
        }

        Invoke("EndOfAttack", attack.time);
    }


    [PunRPC]
    private void TakeDamage(int damage)
    {
        pS.TakeDamage(damage);
    }

    [PunRPC]
    private void Eject(Vector2 force)
    {
        pM.Eject(force);
    }


    private float CalculateForce(float attackForce, int targetPercentage)
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

    //private AvocadoAttacks avocadoAttacks;
    //private PlayerScript playerScript;
    //private PlayerMovement pM;

    //private PlayerScript.Monsters monsterType;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    avocadoAttacks = GetComponent<AvocadoAttacks>();
    //    playerScript = GetComponent<PlayerScript>();
    //    pM = GetComponent<PlayerMovement>();

    //    PlayerScript.Monsters monsterType = playerScript.monster;
    //}

    //// Update is called once per frame
    //void Update()
    //{



    //}

    //public void NormalAttacks()
    //{
    //    if (pM.isGrounded)
    //    {

    //        if (pM.moveInputx != 0)
    //        {
    //            SideG();
    //        }
    //        else if (pM.moveInputy < 0)
    //        {
    //            DownG();
    //        }
    //        else
    //        {
    //            NeutralG();
    //        }


    //    }
    //    else
    //    {
    //        if (pM.moveInputx != 0)
    //        {
    //            SideA();
    //        }
    //        else if (pM.moveInputy < 0)
    //        {
    //            DownA();
    //        }
    //        else
    //        {
    //            NeutralA();
    //        }
    //    }

    //}




    //// Attaques faible au sol (= Weak Ground Attacks) 

    //void NeutralG()
    //{
    //    // Si l'attaquant est l'avocat 
    //    if (monsterType == PlayerScript.Monsters.Avocado)
    //    {
    //        avocadoAttacks.AvocadoNeutralG();

    //    }

    //    // Si l'attaquant est le fantome 
    //    if (monsterType == PlayerScript.Monsters.Ghost)
    //    {
    //        Debug.Log("Attaque du fantome déclenchée (NeutralG)");
    //    }
    //}

    //public void SideG()
    //{

    //    // Si l'attaquant est l'avocat 
    //    if (monsterType == PlayerScript.Monsters.Avocado)
    //    {
    //        avocadoAttacks.AvocadoSideG();

    //    }

    //    // Si l'attaquant est le fantome 
    //    if (monsterType == PlayerScript.Monsters.Ghost)
    //    {
    //        Debug.Log("Attaque du fantome déclenchée");
    //    }


    //}

    //void DownG()
    //{
    //    // Si l'attaquant est l'avocat 
    //    if (monsterType == PlayerScript.Monsters.Avocado)
    //    {
    //        avocadoAttacks.AvocadoDownG();

    //    }

    //    // Si l'attaquant est le fantome 
    //    if (monsterType == PlayerScript.Monsters.Ghost)
    //    {
    //        Debug.Log("Attaque du fantome déclenchée (DownG)");
    //    }

    //}


    //// Attaque faible en l'air (= Air Weak Attacks) 

    //void SideA()
    //{
    //    // Si l'attaquant est l'avocat 
    //    if (monsterType == PlayerScript.Monsters.Avocado)
    //    {
    //        avocadoAttacks.AvocadoSideA();

    //    }

    //    // Si l'attaquant est le fantome 
    //    if (monsterType == PlayerScript.Monsters.Ghost)
    //    {
    //        Debug.Log("Attaque du fantome déclenchée (Side A)");
    //    }
    //}

    //void NeutralA()
    //{
    //    // Si l'attaquant est l'avocat 
    //    if (monsterType == PlayerScript.Monsters.Avocado)
    //    {
    //        avocadoAttacks.AvocadoNeutralA();

    //    }

    //    // Si l'attaquant est le fantome 
    //    if (monsterType == PlayerScript.Monsters.Ghost)
    //    {
    //        Debug.Log("Attaque du fantome déclenchée (NeutralA)");
    //    }
    //}

    //void DownA()
    //{
    //    // Si l'attaquant est l'avocat 
    //    if (monsterType == PlayerScript.Monsters.Avocado)
    //    {
    //        avocadoAttacks.AvocadoDownA();

    //    }

    //    // Si l'attaquant est le fantome 
    //    if (monsterType == PlayerScript.Monsters.Ghost)
    //    {
    //        Debug.Log("Attaque du fantome déclenchée (DownA)");
    //    }
    //}


    //// Fonction qui fais prendre des dégâts à "target". 
    //public void AddPercentage(int damage, PlayerScript target)
    //{

    //    target.percentage += damage;
    //    print(target.name + " took " + damage);
    //    print(target + " current percent: " + target.percentage);

    //}



}
