using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;


public class EnemyManager : MonoBehaviour
{
    //Settings
    public int defaultScore;
    public float defaultDistance;
    public float speedDifference;
    public float defaultSpeedDifference;
    public float positioningDuration = 0.5f;
    bool gameStarted = false;
    public float vignetteDistance = 2.0f;
    // Connections
    EnemySplineController splineController;
    EnemyAnimationManager animationManager;
    EnemyDistanceManager distanceManager;
    SplineFollower playerSplineFollower;
    EnemyFollow enemyFollow;
    public GameObject husbandPoint;
    public VignetteManager vignetteManager;

    public Transform player;
    // State Variables
    public int score;
    public bool reachedFinal = false;
    double playerPercent;
    float speed;
    bool isVignetteOn;
    // Start is called before the first frame update
    void Awake()
    {
        InitConnections();
        InitState();
    }
    void InitConnections(){
        splineController = GetComponent<EnemySplineController>();
        animationManager = GetComponent<EnemyAnimationManager>();
        distanceManager = GetComponent<EnemyDistanceManager>();
        playerSplineFollower = player.GetComponent<SplineFollower>();
        enemyFollow = GetComponent<EnemyFollow>();

        distanceManager.GetDistancePercentEvent += GetDistancePercent;
        distanceManager.SpeedUp += SpeedUp;
        distanceManager.SlowDown += SlowDown;
        distanceManager.StabilizeSpeed += StabilizeSpeed;
        enemyFollow.player = player;
    }
    void InitState(){
        defaultSpeedDifference = speedDifference;
        isVignetteOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerPercent = playerSplineFollower.result.percent;
        distanceManager.playerPercent = playerPercent;
        CheckDistanceForVignetteEffect();

    }
    public void AssignRoad(SplineComputer roadSpline)
    {
        splineController.AssignRoad(roadSpline);
        
    }
    public void StartLevel()
    {
        speed = playerSplineFollower.followSpeed;
        splineController.ChangeSpeed(speed);
        Debug.Log("EM:StartSpeed = " + speed);
        distanceManager.inLevel = true;
        animationManager.Walk();
    }
    public void SendScoreToDistanceManager()
    {
        distanceManager.score = score;
        distanceManager.GetWantedDistance();
        if (!gameStarted)
        {
            distanceManager.defaultScore = defaultScore;
            distanceManager.defaultDistance = defaultDistance;
            gameStarted = true;
        }
    }

    void GetDistancePercent()
    {
        playerPercent = playerSplineFollower.result.percent;
        distanceManager.playerPercent = playerPercent;
        distanceManager.GetStartingDistancePercent();
        distanceManager.SetDistanceCoef();
    }

    void SpeedUp()
    {
        speed = playerSplineFollower.followSpeed;
        speedDifference = defaultSpeedDifference;
        CalculateSpeedDifferenceSpeedUp();
        if (reachedFinal)
            speed = 0;
        splineController.ChangeSpeed(speed);
    }
    void SlowDown()
    {
        speed = playerSplineFollower.followSpeed;
        speedDifference = defaultSpeedDifference;
        CalculateSpeedDifferenceSlowDown();
        if (reachedFinal)
            speed = 0;
        splineController.ChangeSpeed(speed);
    }
    void StabilizeSpeed()
    {
        speed = playerSplineFollower.followSpeed;
        splineController.ChangeSpeed(speed);
    }

    void CalculateSpeedDifferenceSpeedUp()
    {
        if (Mathf.Abs(score - defaultScore) <= 20)
        {
            speed += speedDifference;
        }
        else if(Mathf.Abs(score - defaultScore) <= 40)
        {
            speedDifference *= 2;
            speed += speedDifference;
        }
        else if(Mathf.Abs(score - defaultScore) <= 60)
        {
            speedDifference *= 4;
            speed += speedDifference;
        }
    }
    void CalculateSpeedDifferenceSlowDown()
    {
        if (Mathf.Abs(score - defaultScore) <= 20)
        {
            speed -= speedDifference;
        }
        else if (Mathf.Abs(score - defaultScore) <= 40)
        {
            speedDifference *= 2;
            speed -= speedDifference;
        }
        else if (Mathf.Abs(score - defaultScore) <= 60)
        {
            speedDifference *= 4;
            speed -= speedDifference;
        }
    }

    public void Cry()
    {
        animationManager.Cry();
    }

    public void Cheer()
    {
        animationManager.Cheer();
    }

    public void Threaten()
    {
        animationManager.Threaten();
    }

    public void Court()
    {
        GetComponent<Collider>().enabled = false;
        splineController.StopFollowingSpline();
        Debug.Log(husbandPoint.transform.position);
        enemyFollow.enabled = false;
        Tween husbandTween = transform.DOMove(husbandPoint.transform.position, positioningDuration)
            .OnComplete(animationManager.Idle); 
    }

    public void CollisionFail()
    {
        enemyFollow.enabled = false;
        splineController.ChangeSpeed(0);
        splineController.StopFollowingSpline();
        transform.LookAt(player);
        animationManager.Threaten();
    }
    
    void CheckDistanceForVignetteEffect()
    {
        float distance = Vector3.Distance(transform.position, playerSplineFollower.transform.position);
        if(!isVignetteOn && distance <= vignetteDistance)
        {
            vignetteManager?.StartAlarm();
            isVignetteOn = true;
        }
        else if(isVignetteOn && distance > vignetteDistance)
        {
            vignetteManager?.StopAlarm();
            isVignetteOn = false; 
        }
    }
    public float GetPercent()
    {
            return splineController.GetPercent(); // TODO: cok dolambacli oldu ve ayni get percent'leri player manager'da da yaptik
    }
}


