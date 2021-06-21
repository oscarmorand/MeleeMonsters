using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KrakenWave : MonoBehaviour
{
    public float speed = 20;
    public int damage = 3;
    public float knockback = 100;

    private float _direction;

    private Rigidbody2D rb;
    private PhotonView pV;

    private GameObject _parent;

    public CapsuleCollider2D triggerCollider;
    public CapsuleCollider2D physicsCollider;

    public Transform rayCastPoint;
    public Transform groundPoint;

    public List<GameObject> playersInWave;
    public bool isDestroying = false;

    public float countDown = 0;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        RaycastHit2D isThereGround = Physics2D.Raycast(rayCastPoint.position, Vector2.down, 100f, 1 << 8 | 1 << 10);
        if (!isThereGround)
            DestroyBullet();

        bool isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.3f, 1 << 8 | 1 << 10);
        if(isGrounded)
            rb.velocity = new Vector2(speed * _direction, 0);

        foreach (GameObject go in playersInWave)
        {
            if (!isDestroying)
            {
                go.transform.position = transform.position;
                go.transform.Rotate(Vector3.forward, Time.deltaTime * 80);
            }
        }

        countDown += Time.deltaTime;
        if (countDown >= 3)
            DestroyBullet();
    }

    public void Throw(float direction, GameObject parent)
    {
        _direction = direction;
        _parent = parent;


        Vector3 scale = gameObject.transform.localScale;
        scale.x = _direction;
        gameObject.transform.localScale = scale;

        pV.RPC("SetDirection", RpcTarget.All, _direction);

        rb.velocity = new Vector2(speed*_direction, 0.3f * speed);

        triggerCollider.enabled = true;
        Invoke("EnablePhysics", 0.08f);
    }

    public void EnablePhysics()
    {
        physicsCollider.enabled = true;
    }

    public void DestroyBullet()
    {
        isDestroying = true;
        foreach(GameObject go in playersInWave)
        {
            go.GetComponent<PlayerScript>().isHitStun = false;
            go.transform.root.rotation = Quaternion.Euler(0, 0, 0);
            go.GetComponent<PlayerMovement>().FollowWave(false, "");
        }
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
                    playersInWave.Add(collision.transform.root.gameObject);
                    collision.transform.root.GetComponent<PlayerScript>().isHitStun = true;

                    collision.transform.root.gameObject.GetComponent<PlayerMovement>().FollowWave(true, this.name);
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
            if(!pV.IsMine)
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
    }
}
