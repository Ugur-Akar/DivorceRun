using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;


public class LevelManager : MonoBehaviour
{
    //Settings
    public int RewardMoney = 2000;
    // Connections
    public SplineComputer roadSpline;
    public GameObject husbandPoint;
    public GameObject wifePoint;
    public Transform cameraEndGamePoint;
    public Transform bonusLevelEndPosition;
    public Animator cheaterAnimator;
    public Animator judgeAnimator;
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

    public void Success()
    {
        judgeAnimator.SetTrigger("Success");
    }

    public void Fail()
    {
        judgeAnimator.SetTrigger("Fail");
    }

    public void Fall()
    {
        cheaterAnimator.SetTrigger("Fall");
    }
}
