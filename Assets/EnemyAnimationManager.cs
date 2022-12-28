using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAnimationManager : MonoBehaviour
{
    //Settings

    // Connections
    public Animator animator;
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

    public void Cry()
    {
        animator.SetTrigger("Cry");
    }

    public void Cheer()
    {
        animator.SetTrigger("Cheer");
    }

    public void Threaten()
    {
        animator.SetTrigger("Threaten");
    }

    public void Idle()
    {
        animator.SetTrigger("Idle"); // TODO: idle
    }

    public void Walk()
    {
        animator.SetTrigger("Walk");
    }
}

