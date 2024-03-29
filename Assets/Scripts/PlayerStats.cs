﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int Points = 0;
    public int BestScore = 0;
    public static int Diamonds = 0;
    public int Lifes = 5;
    public int Trash = 2;

    public TextMeshProUGUI PointsText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BestScoreText;
    public TextMeshProUGUI DiamondsText;
    public TextMeshProUGUI LifesText;
    public TextMeshProUGUI TrashText;

    public bool isTreatCaught = false;
    public bool isDiamondCaught = false;
    public bool isTrashCaught = false;
    public bool isLifeLost = false;

    public GameMenu GameMenu;
    public GameObject Texts;

    public int DifficulteIncreaseCounter;

    public static PlayerStats Instance { get { return _instance; } }
    private static PlayerStats _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Update()
    {
        StatChanger();

        PointsText.text = "" + Points;
        ScoreText.text = "Score: " + Points;
        BestScore = PlayerPrefs.GetInt("BestScore");
        Diamonds = PlayerPrefs.GetInt("Diamonds");
        DiamondsText.text = "" + Diamonds;
        LifesText.text = "" + Lifes;
        TrashText.text = "" + Trash;

        if (Points > BestScore)
        {
            BestScore = Points;
            BestScoreText.text = "Best Score: " + Points;

            PlayerPrefs.SetInt("BestScore", BestScore);
        }
        else
        {
            BestScoreText.text = "Best Score: " + BestScore;
        }

        if (Trash <= 0 || Lifes <= 0)
        {
            EndGame();
        }

        if(GameMenu.IsGameEnd == true || GameMenu.IsMainMenu == true)
        {
            Time.timeScale = 0;
            Texts.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            Texts.SetActive(true);
        }
    }

    void StatChanger()
    {
        if (isTreatCaught == true)
        {
            Points += 10;
            DifficulteIncreaseCounter += 1;
            isTreatCaught = false;
        }

        if (isDiamondCaught == true)
        {
            Diamonds += 10;
            PlayerPrefs.SetInt("Diamonds", Diamonds);
            isDiamondCaught = false;
        }

        if (isTrashCaught == true)
        {
            Trash -= 1;
            isTrashCaught = false;
        }

        if (isLifeLost == true)
        {
            Lifes -= 1;
            isLifeLost = false;
        }
    }

    void EndGame()
    {
        GameMenu.EndOfGame();
    }
}
