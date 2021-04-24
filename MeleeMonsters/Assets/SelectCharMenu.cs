using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SelectCharMenu : MonoBehaviour
{
    public int indexPosition = -1;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject toggleSelection;
    private SelectMonsterMenu selectMonsterMenu;
    public bool playerSelected = false;

    void Start()
    {
        if(indexPosition == PhotonNetwork.LocalPlayer.ActorNumber-1)
        {
            toggleSelection.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
        }

        selectMonsterMenu = GameObject.Find("SelectMonsterMenu").GetComponent<SelectMonsterMenu>();
    }

    void Update()
    {
        selectMonsterMenu.setMonsterSelected(playerSelected);
    }

    public void MonsterSelected()
    {
        if (!playerSelected)
        { 
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            playerSelected = true;
        }
        else
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            playerSelected = false;
        }
    }

    
}
