using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFollow : MonoBehaviour
{
    //Settings
    public float followingHorizontalLerpFactor = 0.5f;
    // Connections
    public Transform player;
    // State Variables
    float currentSmoothFollowVelocity;
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
    void LateUpdate()
    {
        float targetX = Mathf.Lerp(transform.position.x, player.position.x, followingHorizontalLerpFactor);
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }
}
