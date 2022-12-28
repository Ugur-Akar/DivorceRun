using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour, SimpleUserTapInputListener
{
    public float speed = 10;
    public float rotationLimitDegree = 60;
    public float rotationCoef = 20;

    public bool canMove = true;
    public bool canSwerve = true;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SimpleUserTapInput>().SetListener(this);
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
                Debug.Log(transform.rotation.eulerAngles.y + direction.x * rotationCoef * Time.deltaTime);
            if (Mathf.Abs(transform.rotation.eulerAngles.y+ direction.x*rotationCoef*Time.deltaTime) < rotationLimitDegree)
            {
                Vector3 rotationVector = new Vector3(0, direction.x*rotationCoef*Time.deltaTime, 0);
                transform.Rotate(rotationVector*rotationCoef*Time.deltaTime);
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