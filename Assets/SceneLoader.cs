using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneLoader : MonoBehaviour
{
    //Settings

    //Connections

    // State Variables
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("IntroAnimation", LoadSceneMode.Single);
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
}
