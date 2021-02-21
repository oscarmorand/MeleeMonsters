using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameObject manager;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManagerPrefab").gameObject;
        gameManager = manager.GetComponent<GameManager>();

        int mNumber = gameManager.monsterNbr;
        gameManager.InitPlayerPrefab(mNumber);
        gameManager.InstantiatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
