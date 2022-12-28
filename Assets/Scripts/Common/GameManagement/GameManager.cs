using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dreamteck.Splines;
using DG.Tweening;
using TMPro;
using ElephantSDK;
public class GameManager : MonoBehaviour
{
    // Settings
    public bool spawnLevel = true;
    public int tutorialLevels;
    public int defaultScore = 50;
    public float defaultDistance = 3;
    public float endGameDuration = 4;
    public int percentIncrease = 100;
    int failThreshold = 33;
    public int moneyGainPerStack = 500;
    // Connections
    public GameObject[] levels;
    public GameObject[] bonusLevels;
    public UIManager ui;
    public PlayerManager playerManager;
    public EnemyManager enemyManager;
    public CameraClosenessControl cameraZoom;
    public GameObject indicator;
    public CheaterUnlocker cheaterUnlocker;
    LevelManager currentLevelManager;
    public TextMeshProUGUI endGameDollarDisplayOnSpinner;
    public GameObject cameraParent;
    GameObject levelGO;
    // State variables
    int currentLevel;
    int score;
    int totalScore;
    int isBonusLevel;
    int levelMoney;
    int moneyCollected;
    int straightLevelIndex;

    private void Awake()    
    {
        InitConnections();
    }

    private void Start()
    {
        InitStates();
    }

    void InitStates()
    {
        enemyManager.defaultScore = defaultScore;
        enemyManager.score = defaultScore;
        enemyManager.defaultDistance = defaultDistance;
        enemyManager.SendScoreToDistanceManager();
        playerManager.score = defaultScore;
        cameraZoom.defaultScore = defaultScore;

        currentLevel = PlayerPrefs.GetInt("Level", 0);
        straightLevelIndex = PlayerPrefs.GetInt("StraightLevel", 0);
        ui.SetLevel(currentLevel);
        LoadLevel();
    }

    void InitConnections()
    {
        ui.OnLevelStart += OnLevelStart;
        ui.OnNextLevel += OnNextLevel;
        ui.OnLevelRestart += OnLevelRestart;

        playerManager.FinalEvent += Final;
        playerManager.CollisionFail += OnLevelFailed;
        playerManager.CollisionFail += CollisionFail;
        playerManager.CourtEvent += Court;
        playerManager.BonusLevelFinalEvent += BonusLevelFinal;
        playerManager.CollectedMoney += CollectedMoney;
    }
    void LoadLevel()
    {
        if (spawnLevel)
        {
            UpdateMoney(0);
            isBonusLevel = PlayerPrefs.GetInt("isBonusLevel", 0);

            if (isBonusLevel == 1)
            {
                currentLevel = PlayerPrefs.GetInt("BonusLevel", 0);
                int prefabIndex = GetPrefabIndex(currentLevel, tutorialLevels, bonusLevels.Length);
                levelGO = Instantiate(bonusLevels[prefabIndex], Vector3.zero, Quaternion.identity);
                currentLevelManager = levelGO.GetComponent<LevelManager>();
                AssignBonusRoad();
            }
            else
            {
                int prefabIndex = GetPrefabIndex(currentLevel, tutorialLevels, levels.Length);
                levelGO = Instantiate(levels[prefabIndex], Vector3.zero, Quaternion.identity);
                currentLevelManager = levelGO.GetComponent<LevelManager>();
                levelMoney = currentLevelManager.RewardMoney;
                endGameDollarDisplayOnSpinner.text = "$" + levelMoney + "k";
                //UpdateMoney(0);
                AssignRoad();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < 50; i++)
            {
                Debug.Log("Prefab index for level " + i + ":" + GetPrefabIndex(i, tutorialLevels, levels.Length));
            }
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.SetInt("isBonusLevel", 1);
        }

        enemyManager.score = playerManager.score;
        enemyManager.SendScoreToDistanceManager();
        cameraZoom.score = playerManager.score;
        CheckUpdateProgress();


    }

    int GetPrefabIndex(int levelIndex, int nInitialLevels, int nLevels)
    {

        int nRepeatingLevels = nLevels - nInitialLevels;
        int prefabIndex = levelIndex;
        if (levelIndex >= nInitialLevels)
        {
            prefabIndex = ((levelIndex - nInitialLevels) % nRepeatingLevels) + nInitialLevels;
        }
        return prefabIndex;

    }
   
    void CollisionFail()
    {
        enemyManager.CollisionFail();
    }

    void OnLevelFailed()
    {
        ui.FailLevel();
        Debug.Log("LEVEL FAILED");
        Debug.Log("Straight level failed: " + straightLevelIndex);
        Elephant.LevelFailed(straightLevelIndex);
    }

    void OnFinishLevel()
    {
        if(score >= failThreshold)
        {
            playerManager.GoodEnd();
            enemyManager.Cry();
            ui.FinishLevel();
            LearnAboutCheater(percentIncrease);
            PlayerPrefs.SetInt("Level", currentLevel + 1);
            PlayerPrefs.SetInt("StraightLevel", straightLevelIndex + 1);
        }
        else
        {
            playerManager.BadEnd();
            enemyManager.Cheer();
            ui.FailLevel();
        }

        Debug.Log("Straight level completed: " + straightLevelIndex);
        Elephant.LevelCompleted(straightLevelIndex);
    }

    void OnLevelStart()
    {
        Debug.Log("LEVEL STARTED");

        playerManager.StartLevel();
        if(PlayerPrefs.GetInt("isBonusLevel", 0) == 0)
        enemyManager.StartLevel();
        Debug.Log("Straight level started: " + straightLevelIndex);
        Elephant.LevelStarted(straightLevelIndex);
    }

    void OnLevelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    void OnNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("showStart", 0);
    }
    void Court()
    {
        score = playerManager.score;
        enemyManager.reachedFinal = true;
        if(PlayerPrefs.GetInt("isBonusLevel",0) == 0)
        enemyManager.Court();

    }
    void Final()
    {
        GetCameraIntoPosition();
        Debug.Log("GM:Final");
        if(score >= 33)
        {
            currentLevelManager.Success();
            if(score > 66)
            {
                UpdateMoney(levelMoney);
            }
        }
        else
        {
            currentLevelManager.Fail();
        }
        ui.ShowScoreBoard();
        indicator.GetComponent<Indicator>().StartShowing(score);
        Invoke(nameof(OnFinishLevel), endGameDuration);
    }

    void AssignRoad()
    {

        playerManager.AssignRoad(currentLevelManager.roadSpline);
        playerManager.SetStartPercent();
        enemyManager.AssignRoad(currentLevelManager.roadSpline);
        playerManager.SetEndPercent();
        playerManager.courtPoint = currentLevelManager.wifePoint;
        enemyManager.husbandPoint = currentLevelManager.husbandPoint;

    }

    void AssignBonusRoad()
    {
        enemyManager.gameObject.SetActive(false);
        playerManager.AssignRoad(currentLevelManager.roadSpline);
        playerManager.SetStartPercent();
        playerManager.bonusPoint = currentLevelManager.bonusLevelEndPosition;
    }

    void BonusLevelFinished()
    {
        //PlayerPrefs.SetInt("isBonusLevel", 0);
        int placeholdInteger = PlayerPrefs.GetInt("BonusLevel", 0);
        placeholdInteger += 1;
        PlayerPrefs.SetInt("BonusLevel", placeholdInteger);
        ui.FinishBonusLevel();
        int moneyEarned = playerManager.moneyCollected * moneyGainPerStack;
        UpdateMoney(moneyEarned);
        ui.DisplayMoney();
        Debug.Log("Straight level completed: " + straightLevelIndex);
        Elephant.LevelCompleted(straightLevelIndex);
        PlayerPrefs.SetInt("StraightLevel", straightLevelIndex + 1);
    }

    void GetCameraIntoPosition()
    {
        cameraParent.GetComponent<FollowPlayer>().enabled = false;
        cameraParent.transform.DOMove(currentLevelManager.cameraEndGamePoint.position, playerManager.positioningDuration);
    }

    void LearnAboutCheater(float percent)
    {
        cheaterUnlocker.SetUnlockPercent(percent);
    }

    void BonusLevelFinal()
    {

        if (playerManager.willKick)
        {
            Debug.Log("Kick");
            playerManager.Kick();
        }
        else
        {
            playerManager.Angry();
        }


        Invoke(nameof(BonusLevelFinished), 3f);//TODO:Magic number
    }
    
    void CheckUpdateProgress()
    {
        ui.UpdateProgess(playerManager.GetPercent());
        if(PlayerPrefs.GetInt("isBonusLevel", 0) == 0)
        ui.UpdateEnemyProgress(enemyManager.GetPercent());
    }

    void UpdateMoney(int money)
    {
        int currentMoney = PlayerPrefs.GetInt("Money", 0);
        currentMoney += money;
        ui.inGameScoreText.text = currentMoney + "k";
        PlayerPrefs.SetInt("Money", currentMoney);

        Debug.Log("curMon = " + currentMoney);
    }

    void CollectedMoney()
    {
        UpdateMoney(moneyGainPerStack);
    }
}