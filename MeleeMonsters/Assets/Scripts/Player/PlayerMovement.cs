using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PhotonView photonView;

    [SerializeField]
    private CharacterController myCC;

    [SerializeField]
    private MonsterP monster;

    private float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = monster.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            BasicMovement();
        }
    }

    void BasicMovement()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            myCC.Move(transform.up * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            myCC.Move(-transform.right * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            myCC.Move(-transform.up * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            myCC.Move(transform.right * Time.deltaTime * movementSpeed);
        }
    }
}
