using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoAttacks : MonoBehaviour
{
    private PlayerAttacks playerAttacks;
    private Animator animator;

    
    //SideG Avocat 
    public Transform attackPointAvocadoSideG;
    public float attackRangeAvocadoSideG;
    //SideA Avocat
    public Transform attackPointAvocadoSideA;
    public float attackRangeAvocadoSideA;
    //DownG Avocat 
    public Transform attackPointAvocadoDownG;
    public float attackRangeAvocadoDownG;
    //DownA Avocat 
    public Transform attackPointAvocadoDownA;
    public float attackRangeAvocadoDownA;
    //NeutralG Avocat 
    public Transform attackPointAvocadoNeutralG;
    public float attackRangeAvocadoNeutralG;
    //NeutralA Avocat 
    public Transform attackPointAvocadoNeutralA;
    public float attackRangeAvocadoNeutralA;


    public LayerMask playerLayers;

    // Start is called before the first frame update
    void Start()
    {
        playerAttacks = GetComponent<PlayerAttacks>();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Side G 
    public void AvocadoSideG()
    {
        //Jouer animation 

        // Detection des enemies à portée 
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPointAvocadoSideG.position, attackRangeAvocadoSideG, playerLayers);

        
        // Application des dégâts 
        foreach (Collider2D colliders in hitColliders)
        {            
           
            GameObject hitObject = colliders.gameObject;
            if (hitObject != gameObject)
            { 
                Debug.Log("Vous avez touché " + colliders.name +"(SideG)");
                PlayerScript player = hitObject.GetComponent<PlayerScript>();
                playerAttacks.AddPercentage(8, player);
            }

        }

    }


    // Side A 
    public void AvocadoSideA()
    {
        //Jouer animation 

        // Detection des enemies à portée 
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPointAvocadoSideA.position, attackRangeAvocadoSideA, playerLayers);


        // Application des dégâts 
        foreach (Collider2D colliders in hitColliders)
        {

            GameObject hitObject = colliders.gameObject;
            if (hitObject != gameObject)
            {
                Debug.Log("Vous avez touché " + colliders.name + "(SideA)");
                PlayerScript player = hitObject.GetComponent<PlayerScript>();
                playerAttacks.AddPercentage(8, player);
            }

        }

    }

    //Down G 
    public void AvocadoDownG()
    {
        //Jouer animation 

        // Detection des enemies à portée 
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPointAvocadoDownG.position, attackRangeAvocadoDownG, playerLayers);


        // Application des dégâts 
        foreach (Collider2D colliders in hitColliders)
        {

            GameObject hitObject = colliders.gameObject;
            if (hitObject != gameObject)
            {
                Debug.Log("Vous avez touché " + colliders.name + "(Down G)");
                PlayerScript player = hitObject.GetComponent<PlayerScript>();
                playerAttacks.AddPercentage(5, player);
            }

        }

    }

    //Down A 
    public void AvocadoDownA()
    {
        //Jouer animation 

        // Detection des enemies à portée 
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPointAvocadoDownG.position, attackRangeAvocadoDownG, playerLayers);


        // Application des dégâts 
        foreach (Collider2D colliders in hitColliders)
        {

            GameObject hitObject = colliders.gameObject;
            if (hitObject != gameObject)
            {
                Debug.Log("Vous avez touché " + colliders.name + "(Down A)");
                PlayerScript player = hitObject.GetComponent<PlayerScript>();
                playerAttacks.AddPercentage(5, player);
            }

        }

    }


    //Neutral G 
    public void AvocadoNeutralG()
    {
        //Jouer animation 

        // Detection des enemies à portée 
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPointAvocadoNeutralG.position, attackRangeAvocadoNeutralG, playerLayers);


        // Application des dégâts 
        foreach (Collider2D colliders in hitColliders)
        {

            GameObject hitObject = colliders.gameObject;
            if (hitObject != gameObject)
            {
                Debug.Log("Vous avez touché " + colliders.name + "(Neutral G)");
                PlayerScript player = hitObject.GetComponent<PlayerScript>();
                playerAttacks.AddPercentage(6, player);
            }

        }

    }

    //Neutral A 
    public void AvocadoNeutralA()
    {
        //Jouer animation 

        // Detection des enemies à portée 
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPointAvocadoNeutralA.position, attackRangeAvocadoNeutralA, playerLayers);


        // Application des dégâts 
        foreach (Collider2D colliders in hitColliders)
        {

            GameObject hitObject = colliders.gameObject;
            if (hitObject != gameObject)
            {
                Debug.Log("Vous avez touché " + colliders.name + "(Neutral A)");
                PlayerScript player = hitObject.GetComponent<PlayerScript>();
                playerAttacks.AddPercentage(6, player);
            }

        }

    }










    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPointAvocadoSideG.position, attackRangeAvocadoSideG);
    }
}
