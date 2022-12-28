using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class CourtPositioner : MonoBehaviour
{
    //Settings
    double lastPointPercent;
    int pointIndex;
    public float xOffset = 1.5f;
    // Connections
    SplinePositioner splinePositioner;
    SplinePoint[] points; 
    // State Variables
    
    // Start is called before the first frame update
    void Awake()
    {
        InitConnections();
        InitState();
    }
    void InitConnections(){
        splinePositioner = GetComponent<SplinePositioner>();
        points = splinePositioner.spline.GetPoints();
    }
    void InitState(){
        pointIndex = points.Length - 2;
        lastPointPercent = splinePositioner.spline.GetPointPercent(pointIndex);

        splinePositioner.motion.offset = new Vector2(xOffset, 0);

        lastPointPercent = (lastPointPercent + 1) / 2;
        GetIntoPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetIntoPosition()
    {
        splinePositioner.SetPercent(lastPointPercent);
    }
}
