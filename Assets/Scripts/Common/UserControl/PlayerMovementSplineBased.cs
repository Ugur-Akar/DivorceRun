using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
public class PlayerMovementSplineBased : MonoBehaviour, SimpleUserTapInputListener
{
    
    // Settings
    public float roadBorder = 2;
    public float speedCoef = 10;
    // Connections
    SplineFollower splineFollower;
    // State variables
    public bool canMove = true;
    public bool canSwerve = true;

    // Start is called before the first frame update

    void Awake()
    {
        InitConnections();
    }

    void InitConnections()
    {
        GetComponent<SimpleUserTapInput>().SetListener(this);
        splineFollower = GetComponent<SplineFollower>();
    }

    void InitState()
    {

    }
    
    void Start()
    {
        InitState();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDirectiveTap(Vector2 direction)
    {
        if (canSwerve)
        {
            direction.y = 0;
            if (Mathf.Abs(splineFollower.motion.offset.x + direction.x) <= roadBorder)
            {
                direction *= Time.deltaTime*speedCoef;
                splineFollower.motion.offset += direction;
            }
                
        }
    }

    public void Disable()
    {
        canMove = false;
        canSwerve = false;
    }

    public void Enable()
    {
        canMove = true;
        canSwerve = true;
    }

}