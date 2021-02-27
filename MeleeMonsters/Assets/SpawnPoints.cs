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

    public Transform p3S1;
    public Transform p3S2;
    public Transform p3S3;
    public List<Transform> p3 = new List<Transform>();

    public Transform p4S1;
    public Transform p4S2;
    public Transform p4S3;
    public Transform p4S4;
    public List<Transform> p4 = new List<Transform>();

    private void Awake()
    {
        p1.Add(p1S1);

        p2.Add(p2S1);
        p2.Add(p2S2);

        p3.Add(p3S1);
        p3.Add(p3S2);
        p3.Add(p3S3);

        p4.Add(p4S1);
        p4.Add(p4S2);
        p4.Add(p4S3);
        p4.Add(p4S1);
    }

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }
}
