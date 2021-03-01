using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private AvocadoAttacks avocadoAttacks;
    private PlayerScript playerScript;

    private PlayerScript.Monsters monsterType;
  
    // Start is called before the first frame update
    void Start()
    {
        avocadoAttacks = GetComponent<AvocadoAttacks>();
        playerScript = GetComponent<PlayerScript>();

        PlayerScript.Monsters monsterType = playerScript.monster;
    }

    // Update is called once per frame
    void Update()
    {
      
     

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

    }

    public void NeutralS()
    {
        print("neutralS");
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

    public void AddPercentage(int damage,PlayerScript target)
    {

        target.percentage += damage;
        print(target.name + " took " + damage);
        print(target.percentage);

    }



}
