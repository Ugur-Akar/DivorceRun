using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimationManager : MonoBehaviour
{
    //Settings

    // Connections
    public Animator animator;
    // State Variables
    
    // Start is called before the first frame update
    void Start()
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

    public void Walk()
    {
        animator.SetTrigger("Walk");
    }

    public void Idle()
    {
        animator.SetTrigger("Idle");
    }
    
    public void Cry()
    {
        animator.SetTrigger("Cry");
    }
    
    public void Success()
    {
        animator.SetTrigger("Success");
    }

    public void Fail()
    {
        animator.SetTrigger("Fail");
    }

    public void Kick()
    {
        animator.SetTrigger("Kick");
    }

    public void Angry()
    {
        animator.SetTrigger("Angry");
    }
}
