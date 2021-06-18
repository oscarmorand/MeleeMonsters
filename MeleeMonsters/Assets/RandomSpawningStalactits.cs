using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawningStalactits : MonoBehaviour
{

    public List<Transform> spawningPoints;

    public GameObject stalactiteGO;

    public float period;

    private float relativeTime = 0;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        relativeTime += Time.deltaTime;
        if(relativeTime >= period)
        {
            relativeTime = 0;
            print("il est l'heure");
            int rand = Random.Range(0, 4);
            print(rand);
            if(rand == 0)
            {
                int randPos = Random.Range((int)0, (int)spawningPoints.Count);
                print("je spawnnnn au " +randPos+" point");
                Vector3 position = spawningPoints[randPos].position;
                SpawningStalactit(position);
            }
        }
    }

    void SpawningStalactit(Vector3 position)
    {
        PhotonNetwork.Instantiate(stalactiteGO.name, position, Quaternion.identity);
    }
}
