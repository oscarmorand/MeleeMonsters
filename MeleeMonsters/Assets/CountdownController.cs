﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public TMP_Text countdownDisplay;
    private GameManager gameManager;

    private GameObject aMGameObject;
    private AudioManager aM;

    string[] countdownStock = {"one", "two", "three"};

    private void Start()
    {
        gameManager = GameObject.Find("GameManagerPrefab").GetComponent<GameManager>();
        StartCoroutine(CountdownToStart());

        aMGameObject = GameObject.Find("AudioManager");
        aM = aMGameObject.GetComponent<AudioManager>();
    }
    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);
            aM.Play(countdownStock[countdownTime - 1]);

            countdownTime--;
        }

        countdownDisplay.text = "MELEE !";

        gameManager.SetGameState(GameManager.States.Playing);

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }
}
