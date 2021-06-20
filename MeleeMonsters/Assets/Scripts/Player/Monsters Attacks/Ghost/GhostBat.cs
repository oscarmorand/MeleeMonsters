using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GhostBat : MonoBehaviour
{
    public float speed = 0.12f;
    public int damage = 3;
    public float knockback = 100;

    private float _direction;

    private Rigidbody2D rb;
    private PhotonView pV;

    private GameObject _parent;

    private float baseX;
    private float cleanX=0;
    private float x;
    private float baseY;
    private float y;

    void Awake()
    {
        //rb.velocity = transform.right * speed * direction;
        rb = GetComponent<Rigidbody2D>();
        pV = GetComponent<PhotonView>();
    }

    public void FixedUpdate()
    {
        cleanX = cleanX + (_direction) * speed;
        x = baseX + cleanX;
        y = baseY + Mathf.Sin(cleanX*1.5f)/1.5f;
        rb.MovePosition(new Vector2(x,y));
    }

    public void Throw(float direction, GameObject parent)
    {
        _direction = direction;
        _parent = parent;
        //rb.velocity = transform.right * speed * _direction;
        baseX = transform.position.x;
        baseY = transform.position.y;
        x = baseX;
        y = baseY;
        

        Invoke("DestroyBullet", 2f);

        GetComponent<CircleCollider2D>().enabled = true;
    }

    public void DestroyBullet()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "IA")
        {
            if (collision.transform.root.gameObject != _parent)
            {
                if (pV.IsMine)
                {
                    PhotonView pVTarget = collision.GetComponent<PhotonView>();
                    float bonus = 1;
                    if (_parent.GetComponent<PlayerScript>().isWrath)
                        bonus = 1.25f;

                    Vector2 ejectionVector = transform.right * _direction;
                    pVTarget.RPC("Eject", RpcTarget.All, ejectionVector, knockback, bonus,damage);

                    pVTarget.RPC("TakeDamage", RpcTarget.All, damage);

                    DestroyBullet();
                }
            }
        }
        else if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10)
        {
            DestroyBullet();
        }
    }
}
