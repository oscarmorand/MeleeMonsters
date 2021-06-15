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

        foreach(GameObject go in playersInWave)
        {
            go.transform.position = transform.position;
            go.transform.Rotate(Vector3.forward, Time.deltaTime * 10);
        }
    }

    public void Throw(float direction, GameObject parent)
    {
        _direction = direction;
        _parent = parent;


        Vector3 scale = gameObject.transform.localScale;
        scale.x *= _direction;
        gameObject.transform.localScale = scale;
        rb.velocity = new Vector2(speed*_direction, 0.3f * speed);

        triggerCollider.enabled = true;
        Invoke("EnablePhysics", 0.08f);

        //Invoke("DestroyBullet", durationTime);
    }

    public void EnablePhysics()
    {
        physicsCollider.enabled = true;
    }

    public void DestroyBullet()
    {
        foreach(GameObject go in playersInWave)
        {
            go.GetComponent<PlayerScript>().isHitStun = false;
            go.transform.rotation = new Quaternion();
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
                }
            }
        }
    }
}
