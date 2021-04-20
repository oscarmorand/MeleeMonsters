using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoAttacks : MonoBehaviour
{

    public List<Attack> attacks;

    //public List<Collider2D> colliders;

    public List<Transform> hitboxesPoints;

    private void Start()
    {
        attacks = new List<Attack>();
        attacks.Add(new Attack("sideWeak", 4, 2, 0.4f, new Vector2(1, 0), hitboxesPoints[0],new Vector2(1,1)));
        attacks.Add(new Attack("neutralWeak", 3, 2, 0.4f, new Vector2(0, 1), hitboxesPoints[0],new Vector2(1,1)));
        attacks.Add(new Attack("downWeak", 4, 2, 0.4f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1,1)));

        attacks.Add(new Attack("sideSpecial", 10, 1, 0.8f, new Vector2(1, 0), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("neutralSpecial", 2, 10, 0.8f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("downSpecial", 5, 5, 0.8f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));

        attacks.Add(new Attack("sideAir", 4, 2, 0.4f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("neutralAir", 4, 2, 0.4f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
        attacks.Add(new Attack("downAir", 4, 2, 0.4f, new Vector2(0, 1), hitboxesPoints[0], new Vector2(1, 1)));
    }



    //private PlayerAttacks playerAttacks;
    //private Animator animator;


    ////SideG Avocat 
    //public Transform attackPointAvocadoSideG;
    //public float attackRangeAvocadoSideG;
    ////SideA Avocat
    //public Transform attackPointAvocadoSideA;
    //public float attackRangeAvocadoSideA;
    ////DownG Avocat 
    //public Transform attackPointAvocadoDownG;
    //public float attackRangeAvocadoDownG;
    ////DownA Avocat 
    //public Transform attackPointAvocadoDownA;
    //public float attackRangeAvocadoDownA;
    ////NeutralG Avocat 
    //public Transform attackPointAvocadoNeutralG;
    //public float attackRangeAvocadoNeutralG;
    ////NeutralA Avocat 
    //public Transform attackPointAvocadoNeutralA;
    //public float attackRangeAvocadoNeutralA;


    //public LayerMask playerLayers;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    playerAttacks = GetComponent<PlayerAttacks>();



    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    ////Side G 
    //public void AvocadoSideG()
    //{
    //    //Jouer animation 

    //    // Detection des enemies à portée 
    //    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPointAvocadoSideG.position, attackRangeAvocadoSideG, playerLayers);


    //    // Application des dégâts 
    //    foreach (Collider2D colliders in hitColliders)
    //    {            

    //        GameObject hitObject = colliders.gameObject;
    //        if (hitObject != gameObject)
    //        { 
    //            Debug.Log("Vous avez touché " + colliders.name +"(SideG)");
    //            PlayerScript player = hitObject.GetComponent<PlayerScript>();

    //            //Application des degats
    //            playerAttacks.AddPercentage(8, player);


    //            //Knockback
    //            /*
    //            Vector2 direction = colliders.transform.position - gameObject.transform.position;
    //            Rigidbody2D rbEnemi = colliders.GetComponent<Rigidbody2D>();
    //            float knockbackstrenght = player.percentage * 0.8f;
    //            rbEnemi.AddForce(direction * knockbackstrenght,ForceMode2D.Impulse);*/
    //        }

    //    }

    //}


    //// Side A 
    //public void AvocadoSideA()
    //{
    //    //Jouer animation 

    //    // Detection des enemies à portée 
    //    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPointAvocadoSideA.position, attackRangeAvocadoSideA, playerLayers);


    //    // Application des dégâts 
    //    foreach (Collider2D colliders in hitColliders)
    //    {

    //        GameObject hitObject = colliders.gameObject;
    //        if (hitObject != gameObject)
    //        {
    //            Debug.Log("Vous avez touché " + colliders.name + "(SideA)");
    //            PlayerScript player = hitObject.GetComponent<PlayerScript>();

    //            //Ajout des degats
    //            playerAttacks.AddPercentage(8, player);



    //        }

    //    }

    //}

    ////Down G 
    //public void AvocadoDownG()
    //{
    //    //Jouer animation 

    //    // Detection des enemies à portée 
    //    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPointAvocadoDownG.position, attackRangeAvocadoDownG, playerLayers);


    //    // Application des dégâts 
    //    foreach (Collider2D colliders in hitColliders)
    //    {

    //        GameObject hitObject = colliders.gameObject;
    //        if (hitObject != gameObject)
    //        {
    //            Debug.Log("Vous avez touché " + colliders.name + "(Down G)");
    //            PlayerScript player = hitObject.GetComponent<PlayerScript>();
    //            playerAttacks.AddPercentage(5, player);
    //        }

    //    }

    //}

    ////Down A 
    //public void AvocadoDownA()
    //{
    //    //Jouer animation 

    //    // Detection des enemies à portée 
    //    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPointAvocadoDownG.position, attackRangeAvocadoDownG, playerLayers);


    //    // Application des dégâts 
    //    foreach (Collider2D colliders in hitColliders)
    //    {

    //        GameObject hitObject = colliders.gameObject;
    //        if (hitObject != gameObject)
    //        {
    //            Debug.Log("Vous avez touché " + colliders.name + "(Down A)");
    //            PlayerScript player = hitObject.GetComponent<PlayerScript>();
    //            playerAttacks.AddPercentage(5, player);
    //        }

    //    }

    //}


    ////Neutral G 
    //public void AvocadoNeutralG()
    //{
    //    //Jouer animation 

    //    // Detection des enemies à portée 
    //    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPointAvocadoNeutralG.position, attackRangeAvocadoNeutralG, playerLayers);


    //    // Application des dégâts 
    //    foreach (Collider2D colliders in hitColliders)
    //    {

    //        GameObject hitObject = colliders.gameObject;
    //        if (hitObject != gameObject)
    //        {
    //            Debug.Log("Vous avez touché " + colliders.name + "(Neutral G)");
    //            PlayerScript player = hitObject.GetComponent<PlayerScript>();
    //            playerAttacks.AddPercentage(6, player);
    //        }

    //    }

    //}

    ////Neutral A 
    //public void AvocadoNeutralA()
    //{
    //    //Jouer animation 

    //    // Detection des enemies à portée 
    //    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPointAvocadoNeutralA.position, attackRangeAvocadoNeutralA, playerLayers);


    //    // Application des dégâts 
    //    foreach (Collider2D colliders in hitColliders)
    //    {

    //        GameObject hitObject = colliders.gameObject;
    //        if (hitObject != gameObject)
    //        {
    //            Debug.Log("Vous avez touché " + colliders.name + "(Neutral A)");
    //            PlayerScript player = hitObject.GetComponent<PlayerScript>();
    //            playerAttacks.AddPercentage(6, player);
    //        }

    //    }

    //}



    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireSphere(attackPointAvocadoSideG.position, attackRangeAvocadoSideG);
    //}
}
