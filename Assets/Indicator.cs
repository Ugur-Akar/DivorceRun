using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Indicator : MonoBehaviour
{
    //Settings
    public float turnTime;
    public float targetAngle;
    public float score;

    float minAngle = -80;
    float maxAngle = 93;
    // Connections

    // State Variables
    float currentAngle;
    public bool canShow = false;
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

    public void StartShowing(float _score)
    {
        score = _score;
        score = score / 100;
        Vector3 wantedVector = Vector3.Lerp(new Vector3(0, 0, maxAngle), new Vector3(0, 0, minAngle), score);
        targetAngle = wantedVector.z;
        
        
        SetTween();
    }

    void SetTween()
    {
        transform.DOLocalRotate(new Vector3(transform.localRotation.x, transform.localRotation.y, targetAngle), turnTime).SetEase(Ease.Linear);
    }
}
