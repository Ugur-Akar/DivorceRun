using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CollisionManager : MonoBehaviour
{
    //Settings

    // Connections
    public event Action<int> BadObstacle;
    public event Action<int> GoodObstacle;
    public event Action Final;
    public event Action CollidedWithEnemy;
    public event Action<bool> StoryGateCollision;
    public event Action CollidedWithMoney;
    // State Variables
    
    // Start is called before the first frame update
    void Start()
    {
        //InitConnections();
        //InitState();
        Taptic.tapticOn = true;
    }
    void InitConnections(){
    }
    void InitState(){
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            
            Taptic.Light();
            Obstacle obs = other.GetComponent<Obstacle>();
            if (obs.isEnemy)
            {
                BadObstacle(obs.score);
            }
            else
            {
                GoodObstacle(obs.score);
            }
            
        }
        else if (other.CompareTag("StoryGate"))
        {
            bool willKick = other.GetComponent<Gate>().willKick;
            StoryGateCollision(willKick);
        }

        if (other.CompareTag("Enemy"))
        {
            CollidedWithEnemy();
        }

        if (other.CompareTag("Money"))
        {
            CollidedWithMoney();
        }
    }

}
