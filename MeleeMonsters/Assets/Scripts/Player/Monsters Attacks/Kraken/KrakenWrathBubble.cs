using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KrakenWrathBubble : MonoBehaviour
{
    public float speed = 12f;
    public int damage = 3;
    public float knockback = 100;
    public float range;

    private float _direction;

    private Rigidbody2D rb;
    private PhotonView pV;
    private CircleCollider2D cC;
    private Animator anim;

    private GameObject _parent;

    private float bonus = 1;

    void Awake()
    {
        //rb.velocity = transform.right * speed * direction;
        rb = GetComponent<Rigidbody2D>();
        pV = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }

    public void Throw(float direction, GameObject parent, float size)
    {
        _direction = direction;
        _parent = parent;
        rb.velocity = transform.right * speed * _direction;

        float resize = 0.8f + Mathf.Clamp(size, 0, 2);
        bonus = resize;
        gameObject.transform.localScale = new Vector3(resize, resize, 1);

        Invoke("DestroyBullet", resize);

        cC = GetComponent<CircleCollider2D>();
        cC.enabled = true;
    }

    public void DestroyBullet()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "IA" || collision.gameObject.layer == 8 || collision.gameObject.layer == 10)
        {
            if (collision.transform != _parent.transform)
            {
                if (pV.IsMine)
                {
                    EndExplosion();
                }
            }
        }
    }

    void EndExplosion()
    {
        cC.enabled = false;
        rb.velocity = Vector2.zero;
        transform.localScale = Vector3.SmoothDamp
        LayerMask layerMask = (1 << 9) | (1 << 11);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range*bonus, layerMask);
        foreach (Collider2D playerCollider in hitColliders)
        {
            if (playerCollider.transform.tag == "Player" || playerCollider.transform.tag == "IA")
            {
                if (pV.IsMine)
                {
                    PhotonView pVTarget = playerCollider.GetComponent<PhotonView>();

                    _parent.GetComponent<PlayerAttacks>().WrathSustain(damage);

                    Vector2 direction = new Vector2(playerCollider.transform.position.x-transform.position.x, playerCollider.transform.position.y - transform.position.y);
                    direction.Normalize();
                    pVTarget.RPC("Eject", RpcTarget.All, direction, knockback, bonus);

                    pVTarget.RPC("TakeDamage", RpcTarget.All, (int)(damage*bonus));
                }
            }
        }
        Invoke("DestroyBullet",0.1f);
    }
}
