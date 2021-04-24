using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;
    public float smoothTime = .3f;

    private Vector3 velocity;
    private Camera cam;

    public float minField = 15f;
    public float maxField = 50f;
    public float zoomLimiter = 80f;


    private void Start()
    {
        InitializeTargets();
        cam = GetComponent<Camera>();
    }


    private void LateUpdate()
    {
        if (targets.Count < PhotonNetwork.CurrentRoom.PlayerCount)
            ActualizeTargets();
        if (targets.Count == 0)
            return;
        Move();
        Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minField, maxField, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    void ActualizeTargets()
    {
        targets = new List<Transform>();
        InitializeTargets();
    }

    void InitializeTargets()
    {
        GameObject[] IAs = GameObject.FindGameObjectsWithTag("IA");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject IA in IAs)
        {
            if(IA.activeInHierarchy)
                targets.Add(IA.transform);
        }
        foreach (GameObject player in players)
        {
            targets.Add(player.transform);
        }
    }

    //public void AddTarget(GameObject target)
    //{
    //    targets.Add(target.transform);
    //}

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
            return targets[0].position;

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
