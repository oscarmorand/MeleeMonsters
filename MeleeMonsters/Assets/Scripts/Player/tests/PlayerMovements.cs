using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HorizontalMovement(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, playerScript.rb.velocity.y);
        playerScript.rb.velocity = Vector3.SmoothDamp(playerScript.rb.velocity, targetVelocity, ref velocity, 0.05f);
    }
}
