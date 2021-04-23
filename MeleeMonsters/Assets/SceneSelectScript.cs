using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SceneSelectScript : MonoBehaviour
{

    public GameObject charaPrefab;

    public List<GameObject> charas = new List<GameObject>();

    private void Start()
    {
        PhotonNetwork.Instantiate(charaPrefab.name, new Vector3(), Quaternion.identity, 0);
    }
}
