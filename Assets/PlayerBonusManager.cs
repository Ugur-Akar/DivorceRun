using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;


public class PlayerBonusManager : MonoBehaviour
{
    //Settings

    // Connections
    public BonusSplineController splineController;
    // State Variables
    
    // Start is called before the first frame update
    void Awake()
    {
        InitConnections();
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
        splineController.AssignRoad(spline);
    }
}
