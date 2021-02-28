using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private AvocadoAttacks avocadoAttacks;
    private PlayerScript playerScript;

  
    // Start is called before the first frame update
    void Start()
    {
        avocadoAttacks = GetComponent<AvocadoAttacks>();
        playerScript = GetComponent<PlayerScript>();

    }

    // Update is called once per frame
    void Update()
    {
      
        // Attaque SideG 
        if (Input.GetKeyDown(KeyCode.J))
        {
            SideG();
        }


    }

    void SideG()
    {
        PlayerScript.Monsters monsterType = playerScript.monster;
        
        // Si l'attaquant est l'avocat 
        if (monsterType == (PlayerScript.Monsters) 0)
        {
            avocadoAttacks.AvocadoSideG();
            
        }

        // Si l'attaquant est le fantome 
        if (monsterType == (PlayerScript.Monsters) 1)
        {
            Debug.Log("Attaque du fantome déclenchée");
        }

    }

    void NeutralG()
    {

    }

    void DownG()
    {

    }

    void SideA()
    {

    }

    void NeutralA()
    {

    }

    void DownA()
    {

    }

    public void AddPercentage(int damage)
    {

        playerScript.percentage += damage;

    }



}
