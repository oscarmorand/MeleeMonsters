using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoAttacks : MonoBehaviour
{
    private PlayerAttacks playerAttacks;
    private Animator animator;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayers;

    // Start is called before the first frame update
    void Start()
    {
        playerAttacks = GetComponent<PlayerAttacks>();
        attackRange = 0.5f;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AvocadoSideG()
    {
        //Jouer animation 

        // Detection des enemies à portée 
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        
        // Application des dégâts 
        foreach (Collider2D colliders in hitColliders)
        {            
            Debug.Log("Vous avez touché" + " " + colliders.name);
            GameObject hitObject = colliders.gameObject;
            PlayerScript player = hitObject.GetComponent<PlayerScript>();
            playerAttacks.AddPercentage(5, player);
            
        }



    }

     void OnDrawGizmosSelected()
    {
       
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
