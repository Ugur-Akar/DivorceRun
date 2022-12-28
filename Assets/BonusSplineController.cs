using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;


public class BonusSplineController : MonoBehaviour
{
    //Settings

    // Connections
    public SplineFollower splineFollower;
    // State Variables
    
    // Start is called before the first frame update
    void Start()
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
        
    }

    public void AssignRoad(SplineComputer spline)
    {
        splineFollower.spline = spline;
    }
}
