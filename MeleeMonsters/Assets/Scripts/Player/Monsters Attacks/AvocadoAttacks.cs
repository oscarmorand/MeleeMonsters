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
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        
        // Application des dégâts 
        foreach (Collider2D player in hitPlayer)
        {
            
            Debug.Log("Vous avez touché" + " " + player.name);
            
        }



    }

     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
