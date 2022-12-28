using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorLerpTest : MonoBehaviour
{
    //Settings
    [Range(0,1)]
    public float weight;
    public Color color1;
    public Color color2;
    // Connections
    Renderer myRenderer;
    // State Variables

    // Start is called before the first frame update
    void Start()
    {
        InitConnections();
        InitState();
    }
    void InitConnections(){
        myRenderer = GetComponent<Renderer>();
    }
    void InitState(){
        myRenderer.material.color = Color.Lerp(color1, color2, weight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        myRenderer.material.color = Color.Lerp(color1, color2, weight);
    }

    
}

