using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraClosenessControl : MonoBehaviour
{
    //Settings
    public float minDistance;
    public float maxDistance;
    public float score;
    public float defaultScore;
    
    int minScore = 1;
    int maxScore = 100;
    // Connections

    // State Variables
    Vector3 targetPosition;
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
    void LateUpdate()
    {
        SetZoomLevel();
        transform.Translate(targetPosition*Time.deltaTime);
        if(transform.localPosition.z > maxDistance)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                maxDistance);
        }
        else if(transform.localPosition.z < minDistance)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                minDistance);
        }

        
    }

    void SetZoomLevel()
    {
        score = score / maxScore;
        targetPosition = Vector3.Lerp(new Vector3(transform.localPosition.x,transform.localPosition.y,maxDistance),
            new Vector3(transform.localPosition.x,transform.localPosition.y,minDistance), score);
        targetPosition = targetPosition - transform.localPosition;
    }
}
