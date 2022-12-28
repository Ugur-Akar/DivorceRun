using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CheaterAnimator : MonoBehaviour
{
    //Settings
    Vector3 jumpTarget = new Vector3(-2.5f, -0.886f, 48);
    int jumpHeight = 4;
    float jumpDuration = 0.5f;
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

    public void Fall()
    {
        animator.SetTrigger("Fall");
        transform.DOJump(jumpTarget, jumpHeight, 1, jumpDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Feet"))
        {
            Fall();
        }
    }
}
