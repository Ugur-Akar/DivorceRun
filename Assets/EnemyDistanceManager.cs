using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System;


public class EnemyDistanceManager : MonoBehaviour
{
    //Settings
    public int defaultScore = 50;
    public double defaultDistance = 3;
    public double startingDistancePercent;
    double distanceCoef;
    // Connections
    public event Action GetDistancePercentEvent;
    public event Action GetOwnPercent;
    public event Action SpeedUp;
    public event Action SlowDown;
    public event Action StabilizeSpeed;
    
    // State Variables
    public int score = 100;
    public double playerPercent;
    public double ownPercent;
    public bool inLevel = false;

    public double wantedDistancePercent;
    double currentDistancePercent;
    // Start is called before the first frame update
    void Awake()
    {
        //InitConnections();
        //InitState();
    }
    void InitConnections(){

    }
    void InitState(){
        
    }

    // Update is called once per frame
    void Update()
    {
        GetWantedDistance();
        GetOwnPercent();
        
    }
    private void LateUpdate()
    {
        if (inLevel)
            CheckDistance();
    }


    void CheckDistance()
    {
        currentDistancePercent = playerPercent - ownPercent;
        if(currentDistancePercent > wantedDistancePercent)
        {
            SpeedUp();
        }
        else if(currentDistancePercent < wantedDistancePercent)
        {
            SlowDown();
        }
        else if(currentDistancePercent == wantedDistancePercent)
        {
            StabilizeSpeed();
        }
    }

    public void GetDistancePercent()
    {
        GetDistancePercentEvent();
    }
    public void SetDistanceCoef()
    {
        distanceCoef = startingDistancePercent / defaultScore;
    }

    public void GetStartingDistancePercent()
    {
        startingDistancePercent = playerPercent;
    }

    public void GetWantedDistance()
    {
        wantedDistancePercent = score * distanceCoef;
    }

    
}
