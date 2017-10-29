using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIController : MonoBehaviour
{
    public Transform pauseScreenCanvas;
    public Transform gameOverCanvas;
    public Button pauseButton;
    public Button gameOverButton;
    public GameControllerScript controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();

        Button btn = pauseButton.GetComponent<Button>();
        gameOverButton.onClick.AddListener(gameOverListerner);
        btn.onClick.AddListener(OnClickListerner);
        gameOverButton.onClick.AddListener(gameOverListerner);
    }

    private void gameOverListerner()
    {
        if (gameOverCanvas.gameObject.activeInHierarchy == false)
        {
            gameOverCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            gameOverCanvas.gameObject.SetActive(false);
            controller.playerHealth = 0;
            Time.timeScale = 1;
        }
    }

    private void OnClickListerner()
    {
        if (pauseScreenCanvas.gameObject.activeInHierarchy == false)
        {
            pauseScreenCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseScreenCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
