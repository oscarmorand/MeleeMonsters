using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    internal bool normalButton;
    internal bool specialButton;

    private PlayerMovement pM;
    private PlayerScript pS;

    internal bool isAttacking = false;
    internal bool canAttack = true;

    public AvocadoAttacks avocadoAttacks;

    public PlayerScript.Monsters monster;

    public void Start()
    {
        pM = GetComponent<PlayerMovement>();
        pS = GetComponent<PlayerScript>();
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
        print("je performe une attaque");
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

        LayerMask layerMask = 9 | 10;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attack.hitBox.position,attack.size, layerMask);
        foreach (Collider2D playerCollider in hitColliders)
        {
            if(playerCollider != null && playerCollider.transform != transform)
            {
                PlayerScript targetScript;
                if (playerCollider.TryGetComponent<PlayerScript>(out targetScript))
                {

                    Vector2 direction = attack.direction;
                    float force = CalculateForce(attack.ejection, targetScript.percentage);
                    PlayerMovement pMTarget = playerCollider.GetComponent<PlayerMovement>();
                    pMTarget.EjectState(new Vector2((direction.x) * force * pM.direction, (direction.y) * force), 0.1f);

                    targetScript.percentage += attack.damage;
                    print("oof " + targetScript.nickName + " a pris une " + attack.name + " d'une puissance de " + attack.damage + " pourcents et monte maintenant à " + targetScript.percentage + " pourcents!");

                }
            }
        }

        Invoke("EndOfAttack", attack.time);
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
