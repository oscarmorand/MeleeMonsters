using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public Transform p1S1;
    public List<Transform> p1 = new List<Transform>();

    public Transform p2S1;
    public Transform p2S2;
    public List<Transform> p2 = new List<Transform>();

    private void Awake()
    {
        p1.Add(p1S1);

        p2.Add(p2S1);
        p2.Add(p2S2);
    }

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }
}
