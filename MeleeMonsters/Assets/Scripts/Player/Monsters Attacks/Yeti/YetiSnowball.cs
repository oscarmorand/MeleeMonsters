﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class YetiSnowball : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 3;
    public float knockback = 100;
    public float durationTime = 2f;

    private float _direction;

    private Rigidbody2D rb;
    private PhotonView pV;
    private AudioManager aM;

    private GameObject _parent;

    void Awake()
    {
        //rb.velocity = transform.right * speed * direction;
        rb = GetComponent<Rigidbody2D>();
        pV = GetComponent<PhotonView>();
        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Throw(float direction, GameObject parent)
    {
        _direction = direction;
        _parent = parent;
        rb.velocity = new Vector2(speed * _direction, 0.5f* speed);

        Invoke("DestroyBullet", durationTime);

        GetComponent<CircleCollider2D>().enabled = true;
    }

    public void DestroyBullet()
    {
        aM.Play("snowball");
        PhotonNetwork.Destroy(gameObject);
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

                    Vector2 ejectionVector = transform.right * _direction;
                    pVTarget.RPC("Eject", RpcTarget.All, ejectionVector, knockback, 1f, damage);

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
