using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCollisions : MonoBehaviour
{

    private PlayerMovement pM;
    private PhotonView pV;
    private PlayerScript pS;
    private Rigidbody2D rb;

    public Transform groundCheck;
    public Transform frontCheck;
    public Transform upCheck;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlatform;

    public float checkRadius;


    // Start is called before the first frame update
    void Start()
    {
        pM = GetComponent<PlayerMovement>();
        pV = GetComponent<PhotonView>();
        pS = GetComponent<PlayerScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckOverlap();
    }

    void CheckOverlap()
    {
        pM.isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        pM.isOnPlatform = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsPlatform);
        pM.HasPassedPlatform = Physics2D.OverlapCircle(upCheck.position, checkRadius, whatIsPlatform);

        pM.isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 12)
        {
            GameObject attacker = collision.transform.root.gameObject;

            Attack attack = attacker.GetComponent<PlayerAttacks>().currentAttack;

            print(attacker.name + " m'attaque avec une "+attack.name);

            TakeAttack(attack, attacker);
        }
     
    }

    public void TakeAttack(Attack attack, GameObject attacker)
    {
        PlayerScript attackerScript = attacker.GetComponent<PlayerScript>();
        PlayerMovement attackerMovement = attacker.GetComponent<PlayerMovement>();

        float bonus = 1;
        if (attackerScript.isWrath)
        {
            bonus = 1.25f;
            attackerScript.WrathSustain(attack.damage);
        }
        int newDamage = (int)((attack.damage) * bonus);

        Vector2 direction = attack.direction;
        Vector2 newDirection = new Vector2((direction.x) * attackerMovement.direction, (direction.y));
        pV.RPC("Eject", RpcTarget.All, newDirection, attack.ejection, bonus);

        pV.RPC("TakeDamage", RpcTarget.All, newDamage);

        pV.RPC("HitStunState", RpcTarget.All, attack.ejection);
    }

    [PunRPC]
    private void TakeDamage(int damage)
    {
        pS.TakeDamage(damage);
    }

    [PunRPC]
    private void Eject(Vector2 direction, float force, float bonus)
    {
        float factor = PlayerAttacks.CalculateForce(force, pS.percentage, rb.mass, bonus);
        pM.Eject(direction * factor);
    }

    [PunRPC]
    private void HitStunState(float knockback)
    {
        float hitStunTime = PlayerAttacks.CalculateHitStun(knockback);
        StartCoroutine(pS.HitStunState(hitStunTime));
    }

}
