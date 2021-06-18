using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    private LevelManager levelManager;

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
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        InitializeTargets();
        cam = GetComponent<Camera>();
    }


    private void LateUpdate()
    {
        //if (targets.Count < levelManager.playersScripts.Count )//PhotonNetwork.CurrentRoom.PlayerCount)
        ActualizeTargets();
        /*
        foreach (Transform target in targets)
        {
            if (target == null)
                ActualizeTargets();
        }
        */
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
            if(targets[i] != null)
                bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x + bounds.size.y;
    }

    void ActualizeTargets()
    {
        targets = new List<Transform>();
        InitializeTargets();
    }

    void InitializeTargets()
    {
        GameObject[] IAs = GameObject.FindGameObjectsWithTag("IA");
        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject IA in IAs)
        {
            if(IA.activeInHierarchy)
                targets.Add(IA.transform);
        }
        foreach (PlayerScript player in levelManager.playersScripts)
        {
            if (player != null)
            {
                if (player.lives > 0)
                    targets.Add(player.transform);
            }
        }
    }

    //public void AddTarget(GameObject target)
    //{
    //    targets.Add(target.transform);
    //}

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            if (targets[0] != null)
                return targets[0].position;
            else
                return Vector3.zero;
        }
            

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if(targets[i] != null)
                bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
