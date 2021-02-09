using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PhotonView PV;

    [SerializeField]
    private CharacterController myCC;

    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
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
