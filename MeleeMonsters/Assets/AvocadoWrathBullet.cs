using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AvocadoWrathBullet : MonoBehaviour
{
    public float speed;
    public int damage = 3;
    public float knockback = 100;
    public float durationTime = 2f;

    private float _direction;

    private Rigidbody2D rb;
    private PhotonView pV;

    private GameObject _parent;

    void Awake()
    {
        //rb.velocity = transform.right * speed * direction;
        rb = GetComponent<Rigidbody2D>();
        pV = GetComponent<PhotonView>();
    }

    public void Throw(float direction, GameObject parent)
    {
        _direction = direction;
        _parent = parent;
        rb.velocity = transform.right * speed * _direction;

        Invoke("DestroyBullet", durationTime);

        GetComponent<CircleCollider2D>().enabled = true;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "IA")
        {
            if (collision.transform != _parent.transform)
            {
                if (pV.IsMine)
                {
                    PhotonView pVTarget = collision.GetComponent<PhotonView>();
                    float bonus = 1;
                    if (_parent.GetComponent<PlayerScript>().isWrath)
                        bonus = 1.25f;

                    Vector2 ejectionVector = transform.right * _direction;
                    pVTarget.RPC("Eject", RpcTarget.All, ejectionVector, knockback, bonus);

                    pVTarget.RPC("TakeDamage", RpcTarget.All, damage);

                    DestroyBullet();
                }
            }
        }
    }
}
