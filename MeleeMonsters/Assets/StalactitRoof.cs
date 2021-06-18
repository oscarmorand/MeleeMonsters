using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactitRoof : MonoBehaviour
{

    public float knockback;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "IA")
        {
            PhotonView pVTarget = collision.GetComponent<PhotonView>();
            float bonus = 1;

            pVTarget.RPC("Eject", RpcTarget.All, Vector2.down, knockback, bonus);

            pVTarget.RPC("TakeDamage", RpcTarget.All, damage);
        }
    }
}
