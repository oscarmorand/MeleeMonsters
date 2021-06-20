using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KrakenHiddenGeyser : MonoBehaviour
{
    public float speed = 12;
    public int damage = 3;
    public float knockback = 100;

    private float _direction;

    private Rigidbody2D rb;
    private PhotonView pV;

    private GameObject _parent;

    public BoxCollider2D triggerCollider;

    public Transform rayCastPoint;
    public Transform groundPoint;

    public bool hasTouched = false;
    public GameObject particleSystemGO;
    public GameObject particleSystemGeyserGO;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        RaycastHit2D isThereGround = Physics2D.Raycast(rayCastPoint.position, Vector2.down, 1f, 1 << 8 | 1 << 10);
        if (!isThereGround)
            DestroyBullet();

        if (!hasTouched)
            rb.velocity = new Vector2(speed * _direction, 0);
        else
            rb.velocity = Vector2.zero;
    }

    public void Throw(float direction, GameObject parent)
    {
        _direction = direction;
        _parent = parent;


        Vector3 scale = gameObject.transform.localScale;
        scale.x = _direction;
        gameObject.transform.localScale = scale;
        Vector3 PsScale = particleSystemGO.transform.localScale;
        PsScale.x = _direction;
        particleSystemGO.transform.localScale = PsScale;

        pV.RPC("SetDirection", RpcTarget.All, _direction);

        rb.velocity = new Vector2(speed * _direction, 0);

        triggerCollider.enabled = true;
    }


    public void RevealGeyser()
    {
        hasTouched = true;
        particleSystemGeyserGO.SetActive(true);
        Invoke("DestroyBullet", 0.82f);
    }

    public void DestroyBullet()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "IA")
        {
            if (collision.transform.root.gameObject != _parent.transform.root.gameObject)
            {
                if (pV.IsMine)
                {
                    PhotonView pVTarget = collision.GetComponent<PhotonView>();

                    Vector2 ejectionVector = Vector2.up;
                    pVTarget.RPC("Eject", RpcTarget.All, ejectionVector, knockback, 1f, damage);

                    pVTarget.RPC("TakeDamage", RpcTarget.All, damage);

                    RevealGeyser();
                }
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
            stream.SendNext(transform.position);
        else
        {
            if (!pV.IsMine)
            {
                Vector3 latestPos = (Vector3)stream.ReceiveNext();
                transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * 5);
            }
        }
    }

    [PunRPC]
    public void SetDirection(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = direction;
        transform.localScale = scale;
        Vector3 PsScale = particleSystemGO.transform.localScale;
        PsScale.x = _direction;
        particleSystemGO.transform.localScale = PsScale;
    }
}
