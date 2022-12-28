using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    const float DEFAULT_START_TIME = 0.1f;
    int cheaterUnlockPercent = 0;

    public  Action OnLevelStart, OnNextLevel, OnLevelRestart;

    [Header("Screens")]
    public GameObject startCanvas;
    public GameObject ingameCanvas;
    public GameObject finishCanvas;
    public GameObject failCanvas;
    public GameObject levelEndCanvas;
    public GameObject bonusEndCanvas;
    [Header("In Game")]
    public LevelBarDisplay levelBarDisplay;
    public TextMeshProUGUI inGameScoreText;
    [Header("Finish Screen")]
    public ScoreTextManager scoreText;
    public TextMeshProUGUI startMoney;

    public TextMeshProUGUI[] endGameMoneyTexts;

    void Start()
    {
        CheckAndDisplayStartScreen();
    }

    private void Awake()
    {
        int mon = PlayerPrefs.GetInt("Money", 0);
        startMoney.text = "" + mon;
    }

    void CheckAndDisplayStartScreen()
    {
        int displayStart = PlayerPrefs.GetInt("displayStart", 1);
        if(displayStart > 0)
        {
            startCanvas.SetActive(true);
        }
        else
        { 
            StartLevel();
            Invoke(nameof(StartLevelButton),DEFAULT_START_TIME);
            PlayerPrefs.SetInt("displayStart", 1);
        }
    }

    #region Handler Functions

    public void StartLevelButton()
    {
        OnLevelStart?.Invoke();
        
    }

    public void NextLevelButton()
    {    
        PlayerPrefs.SetInt("displayStart", 0);
        OnNextLevel?.Invoke();
    }

    public void BonusNextLevelButton()
    {
        PlayerPrefs.SetInt("isBonusLevel", 0);
        OnNextLevel?.Invoke();
    }

    public void RestartLevelButton()
    {
        PlayerPrefs.SetInt("displayStart", 0);
        OnLevelRestart?.Invoke();
    }

    #endregion

    public void StartLevel()
    {
        startCanvas.SetActive(false);
        ingameCanvas.SetActive(true);
    }

    public void SetInGameScore(int score)
    {
        inGameScoreText.text = "" + score;
    }

    public void SetInGameScoreAsText(string scoreText)
    {
        inGameScoreText.text = scoreText;
    }


    public void DisplayScore(int score, int oldScore=0)
    {
        scoreText.DisplayScore(score, oldScore);
    }

    public void SetLevel(int level)
    {
        levelBarDisplay.SetLevel(level);
    }

    public void UpdateProgess(float progress)
    {
        levelBarDisplay.DisplayProgress(progress);
    }

    public void UpdateEnemyProgress(float progress)
    {
        levelBarDisplay.DisplayEnemyProgress(progress);
    }

    public void FinishBonusLevel()
    {
        //levelEndCanvas.SetActive(false);
        ingameCanvas.SetActive(false);
        bonusEndCanvas.SetActive(true);
        DisplayMoney();
    }

    public void FinishLevel()
    {
        levelEndCanvas.SetActive(false);
        ingameCanvas.SetActive(false);
        finishCanvas.SetActive(true);
        DisplayMoney();
    }

    public void FailLevel()
    {
        levelEndCanvas.SetActive(false);
        ingameCanvas.SetActive(false);
        failCanvas.SetActive(true);
    }

    public void ShowScoreBoard()
    {
        ingameCanvas.SetActive(false);
        levelEndCanvas.SetActive(true);
    }


    void InitStates()
    {
        ingameCanvas.SetActive(false);
        finishCanvas.SetActive(false);
        failCanvas.SetActive(false);
        startCanvas.SetActive(true);
        levelEndCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayMoney()
    {
        int money = PlayerPrefs.GetInt("Money", 0);
        foreach (TextMeshProUGUI moneyText in endGameMoneyTexts)
        {
            moneyText.text = money + "k";
        }
    }

    
}
