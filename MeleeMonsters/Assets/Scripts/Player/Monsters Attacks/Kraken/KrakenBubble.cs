using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KrakenBubble : MonoBehaviour
{
    public float speed = 12f;
    public int damage = 3;
    public float knockback = 100;

    private float _direction;

    private Rigidbody2D rb;
    private PhotonView pV;

    private GameObject _parent;

    private float bonus = 1;

    public GameObject particleBurstPrefab;

    void Awake()
    {
        //rb.velocity = transform.right * speed * direction;
        rb = GetComponent<Rigidbody2D>();
        pV = GetComponent<PhotonView>();
    }

    public void Throw(float direction, GameObject parent, float size)
    {
        _direction = direction;
        _parent = parent;
        rb.velocity = transform.right * speed * _direction;

        float resize = 0.8f + Mathf.Clamp(size,0,2);
        bonus = resize;
        gameObject.transform.localScale = new Vector3(resize, resize, 1);

        Invoke("DestroyBullet", resize);

        GetComponent<CircleCollider2D>().enabled = true;
    }

    public void DestroyBullet()
    {
        particleBurstPrefab = PhotonNetwork.Instantiate(particleBurstPrefab.name, transform.position, new Quaternion());
        PlayParticleSystem();
        PhotonNetwork.Destroy(gameObject);
    }

    public void PlayParticleSystem()
    {
        ParticleSystem partSys = particleBurstPrefab.GetComponent<ParticleSystem>();

        var main = partSys.main;
        main.startSize = 0.1f * bonus;

        ParticleSystem.Burst burst = new ParticleSystem.Burst();
        burst.count = 20 * bonus;
        partSys.emission.SetBurst(0, burst);

        partSys.Play();
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

                    Vector2 ejectionVector = transform.right * _direction;
                    pVTarget.RPC("Eject", RpcTarget.All, ejectionVector, knockback, bonus);

                    pVTarget.RPC("TakeDamage", RpcTarget.All, (int)(damage*bonus));

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
