using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System;


public class EnemySplineController : MonoBehaviour
{
    //Settings

    // Connections
    SplineFollower splineFollower;
    EnemyDistanceManager distanceManager;
    

    // State Variables
    public float currentFollowSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        InitConnections();
        //InitState();
    }
    void InitConnections(){
        distanceManager = GetComponent<EnemyDistanceManager>();

        distanceManager.GetOwnPercent += GetOwnPercent;
    }
    void InitState(){
    }

    // Update is called once per frame
    void Update()
    {
        currentFollowSpeed = splineFollower.followSpeed;
        
    }

    public void AssignRoad(SplineComputer roadSpline)
    {
        splineFollower = GetComponent<SplineFollower>();
        splineFollower.spline = roadSpline;
        distanceManager.GetDistancePercent();
    }

    public void ChangeSpeed(float speedIn) {
        splineFollower.followSpeed = speedIn;
    }

    void GetOwnPercent()
    {
        distanceManager.ownPercent = splineFollower.result.percent;
    }

    public float GetPercent()
    {
        return (float)splineFollower.result.percent;
    }

    public void StopFollowingSpline()
    {
        splineFollower.follow = false;
        splineFollower.motion.applyPositionX = false;
    }
}
