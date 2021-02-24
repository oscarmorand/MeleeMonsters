using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public string pseudo;

    public int lives;
    public int percentage;

    public bool isAlive;
    public bool isInGame;
    public bool isWrath;

    private PlayerMovement pMovement;
    private PlayerCollisions pCollisions;
    private PlayerAnimation pAnimation;
    private PlayerInputs pInputs;


    // Start is called before the first frame update
    void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
        pCollisions = GetComponent<PlayerCollisions>();
        pAnimation = GetComponent<PlayerAnimation>();
        pInputs = GetComponent<PlayerInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
