using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dreamteck.Splines;
using DG.Tweening;

public class BonusLevelGameManager : MonoBehaviour
{
    // Settings
    public bool spawnLevel = true;
    public int tutorialLevels;
    public int defaultScore = 50;
    public float defaultDistance = 3;
    public float endGameDuration = 4;

    // Connections
    public GameObject[] levels;
    public GameObject[] bonusLevels;
    public UIManager ui;
    public PlayerBonusManager playerManager;
    LevelManager currentLevelManager;

    public GameObject cameraParent;
    GameObject levelGO;
    // State variables
    int currentLevel;
    int score;
    int totalScore;
    int isBonusLevel;


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
        currentLevel = PlayerPrefs.GetInt("Level", 0);
        LoadLevel();
    }

    void InitConnections()
    {
        ui.OnLevelStart += OnLevelStart;
        ui.OnNextLevel += OnNextLevel;
        ui.OnLevelRestart += OnLevelRestart;

    }
    void LoadLevel()
    {
        if (spawnLevel)
        {
            isBonusLevel = PlayerPrefs.GetInt("isBonusLevel", 0);

            if (isBonusLevel == 1)
            {
                int prefabIndex = GetPrefabIndex(currentLevel, tutorialLevels, bonusLevels.Length);
                levelGO = Instantiate(bonusLevels[prefabIndex], Vector3.zero, Quaternion.identity);
                currentLevelManager = levelGO.GetComponent<LevelManager>();
            }
            else
            {
                int prefabIndex = GetPrefabIndex(currentLevel, tutorialLevels, levels.Length);
                levelGO = Instantiate(levels[prefabIndex], Vector3.zero, Quaternion.identity);
                currentLevelManager = levelGO.GetComponent<LevelManager>();
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

    }

    void OnLevelFailed()
    {
        ui.FailLevel();
        Debug.Log("LEVEL FAILED");
    }

    void OnFinishLevel()
    {
        if (score >= 33)
        {
            ui.FinishLevel();
            PlayerPrefs.SetInt("Level", currentLevel + 1);
        }
        else
        {
            ui.FailLevel();
        }

    }

    void OnLevelStart()
    {
        Debug.Log("LEVEL STARTED");

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

    }
    void Final()
    {
        GetCameraIntoPosition();
        Debug.Log("GM:Final");
        if (score >= 33)
        {
            currentLevelManager.Success();
        }
        else
        {
            currentLevelManager.Fail();
        }
        ui.ShowScoreBoard();
        Invoke(nameof(OnFinishLevel), endGameDuration);
    }

    void AssignRoad()
    {

    }

    void BonusLevelFinished()
    {
        PlayerPrefs.SetInt("isBonusLevel", 0);
    }

    void GetCameraIntoPosition()
    {
        cameraParent.GetComponent<FollowPlayer>().enabled = false;
        cameraParent.transform.DOMove(currentLevelManager.cameraEndGamePoint.position, 1);
    }

}