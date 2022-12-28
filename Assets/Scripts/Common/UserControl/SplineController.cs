using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System;


public class SplineController : MonoBehaviour
{
    //Settings
    public double finalPercent = 1;
    SplinePoint[] pointArray;
    // Connections
    SplineFollower splineFollower;
    public SplineComputer splineComputer;

    public event Action Final;
    public event Action BonusLevelFinal;
    // State Variables
    bool inEndGame = false;

    // Start is called before the first frame update
    void Awake()
    {
        InitConnections();
    }
    private void Start()
    {
        InitState();
    }
    void InitConnections()
    {

    }
    void InitState()
    {
        //splineFollower.follow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inEndGame && splineFollower.result.percent >= finalPercent)
        {
            inEndGame = true;
            Final();
        }
    }
    public void AssignRoad()
    {
        splineFollower = GetComponent<SplineFollower>();

        splineFollower.spline = splineComputer;
        Debug.Log("Road Assigned");
    }

    public void ChangeSpeed(float speedIn)
    {
        splineFollower.followSpeed = speedIn;
    }

    public void SetPercent()
    {
        splineFollower.startPosition = splineFollower.spline.GetPointPercent(1);
    }

    public float GetPercent()
    {
        return (float)splineFollower.result.percent;
    }

    public void SetFinalPercent()
    {
        pointArray = splineFollower.spline.GetPoints();
        int stopPointIndex = pointArray.Length - 2;
        finalPercent = splineFollower.spline.GetPointPercent(stopPointIndex);
    }

    public void StopFollowingSpline()
    {
        splineFollower.follow = false;
    }
}

    
