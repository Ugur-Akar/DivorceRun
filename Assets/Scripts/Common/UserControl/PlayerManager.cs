using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Dreamteck.Splines;
using DG.Tweening;


public class PlayerManager : MonoBehaviour
{
    //Settings
    public float speed = 4;
    const int minScore = 1;
    public float positioningDuration = 0.5f;
    // Connections
    CollisionManager collisionManager;
    PlayerAnimationManager playerAnimationManager;
    SplineController splineController;
    PlayerMovementSplineBased playerMovement;
    public PlayerRotation playerRotation;
    public GameObject courtPoint;
    public Transform bonusPoint;
    public Collider[] FootColliders;

    public event Action FinalEvent;
    public event Action CollisionFail;
    public event Action CourtEvent;
    public event Action BonusLevelFinalEvent;
    public event Action CollectedMoney;
    // State Variables
    public int score = 50;
    public bool willKick = false;
    public int moneyCollected = 0;
    // Start is called before the first frame update
    void Awake()
    {
        InitConnections();      
    }

    void Start()
    {
         //InitState();
    }
    void InitConnections(){
        collisionManager = GetComponent<CollisionManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        splineController = GetComponent<SplineController>();
        playerMovement = GetComponent<PlayerMovementSplineBased>();

        collisionManager.GoodObstacle += GoodObstacle;
        collisionManager.BadObstacle += BadObstacle;
        collisionManager.CollidedWithEnemy += CollidedWithEnemy;
        collisionManager.StoryGateCollision += StoryGate;
        collisionManager.CollidedWithMoney += CollidedWithMoney;

        splineController.Final += Final;
    }
    void InitState(){
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel()
    {
        playerMovement.Enable();
        //playerRotation.Enable();
        splineController.ChangeSpeed(speed);
        playerAnimationManager.Walk();
        GetComponent<Collider>().enabled = true;
    }

    public void AssignRoad(SplineComputer splineComp)
    {
        splineController.splineComputer = splineComp;
        splineController.AssignRoad();
    }

    void BadObstacle(int obsScore)
    {
        //Decrease Score
        score -= obsScore;
        if(score <= minScore)
        {
            score = minScore;
        }
    }

    void GoodObstacle(int obsScore)
    {
        //Increase Score
        score += obsScore;
        if(score >= 100)
        {
            score = 100;
        }
    }

    void Final()
    {   
        Debug.Log("PM:Final");
        if(PlayerPrefs.GetInt("isBonusLevel", 0) == 0)
        {
            CourtEvent();
        }

        splineController.StopFollowingSpline();
        playerMovement.Disable();
        if (PlayerPrefs.GetInt("isBonusLevel", 0) == 0)
        {
            Tween wifeTween = transform.DOMove(courtPoint.transform.position, positioningDuration)
                .OnComplete(playerAnimationManager.Idle);
            Invoke(nameof(CallFinalEvent), positioningDuration);
        }
        else
        {
            splineController.StopFollowingSpline();
            transform.DORotate(new Vector3(0, -90, 0), positioningDuration);
            MoveToBonusEndPosition();
        }
    }

    public void SetStartPercent()
    {
        splineController.SetPercent();
    }

    public void SetEndPercent()
    {
        splineController.SetFinalPercent();
    }

    public float GetPercent()
    {
        return splineController.GetPercent();
    }

    void CollidedWithEnemy()
    {
        Debug.Log("PM:Collided With Enemy");
        playerMovement.Disable();
        splineController.ChangeSpeed(0);
        playerAnimationManager.Cry();
        CollisionFail();
    }

    public void GoodEnd()
    {
        playerAnimationManager.Success();
    }

    public void BadEnd()
    {
        playerAnimationManager.Fail();
    }

    void CallFinalEvent()
    {
        FinalEvent();
    }

    void MoveToBonusEndPosition()
    {
        Tween bonusTween = transform.DOMove(bonusPoint.transform.position, positioningDuration);
                //.OnComplete(playerAnimationManager.Idle);
        Debug.Log("Moved to end pos");
        Invoke(nameof(CallBonusFinalEvent), positioningDuration);
    }

    void CallBonusFinalEvent()
    {
        BonusLevelFinalEvent();
    }

    public void Kick()
    {
        EnableFootColliders();
        playerAnimationManager.Kick();
    }
    public void Angry()
    {
        playerAnimationManager.Angry();
    }

    void StoryGate(bool willKick)
    {
        this.willKick = willKick;
    }

    void CollidedWithMoney()
    {
        CollectedMoney();
    }

    void EnableFootColliders()
    {
        foreach(Collider col in FootColliders)
        {
            col.enabled = true;
            
        }
    }
}
