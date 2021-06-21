using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactitRoof : MonoBehaviour
{

    public float knockback;
    public int damage;
    public bool down;
    public GameObject burst;
    public Transform burstPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "IA")
        {
            PhotonView pVTarget = collision.GetComponent<PhotonView>();

            Vector2 trajectoire;
            if (down)
                trajectoire = Vector2.down;
            else
            {
                if (collision.transform.position.x - transform.position.x > 0)
                    trajectoire = Vector2.right;
                else
                    trajectoire = Vector2.left;
            }

            pVTarget.RPC("Eject", RpcTarget.All, trajectoire, knockback, 1f, damage);

            pVTarget.RPC("TakeDamage", RpcTarget.All, damage);
        }
        else if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10)
        {
            burst = PhotonNetwork.Instantiate(burst.name, burstPos.position, new Quaternion());
            burst.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
        }
    }


}
