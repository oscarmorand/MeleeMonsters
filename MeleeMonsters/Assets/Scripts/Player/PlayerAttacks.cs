using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private AvocadoAttacks avocadoAttacks;
    private PlayerScript playerScript;
    private PlayerMovement pM;

    private PlayerScript.Monsters monsterType;
  
    // Start is called before the first frame update
    void Start()
    {
        avocadoAttacks = GetComponent<AvocadoAttacks>();
        playerScript = GetComponent<PlayerScript>();
        pM = GetComponent<PlayerMovement>();

        PlayerScript.Monsters monsterType = playerScript.monster;
    }

    // Update is called once per frame
    void Update()
    {
      
     

    }

    public void NormalAttacks()
    {
        if (pM.isGrounded)
        {

            if (pM.moveInputx != 0)
            {
                SideG();
            }
            else if (pM.moveInputy < 0)
            {
                DownG();
            }
            else
            {
                NeutralG();
            }
            

        } 
        else
        {
            if (pM.moveInputx != 0)
            {
                SideA();
            }
            else if (pM.moveInputy < 0)
            {
                DownA();
            }
            else
            {
                NeutralA();
            }
        }
        
    }

   

    
    // Attaques faible au sol (= Weak Ground Attacks) 
    
    void NeutralG()
    {
        print("NeutralG");
    }

    public void SideG()
    {
        
        // Si l'attaquant est l'avocat 
        if (monsterType == PlayerScript.Monsters.Avocado)
        {
            avocadoAttacks.AvocadoSideG();

        }

        // Si l'attaquant est le fantome 
        if (monsterType == PlayerScript.Monsters.Ghost)
        {
            Debug.Log("Attaque du fantome déclenchée");
        }

        print("SideG");
    }

    void DownG()
    {
        print("DownA");
    }


    // Attaque faible en l'air (= Air Weak Attacks) 

    void SideA()
    {
        print("SideA");
    }

    void NeutralA()
    {
        print("NeutralA");
    }

    void DownA()
    {
        print("DownA");
    }


    // Fonction qui fais prendre des dégâts à "target". 
    public void AddPercentage(int damage,PlayerScript target)
    {

        target.percentage += damage;
        print(target.name + " took " + damage);
        print(target.percentage);

    }



}
