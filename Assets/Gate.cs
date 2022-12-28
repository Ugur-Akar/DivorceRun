using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Gate : MonoBehaviour
{
    //Settings
    public bool willKick = false;
    // Connections
    public event Action DisableColliders;
    public Material selectedMaterial;
    Renderer myRenderer;
    // State Variables

    // Start is called before the first frame update
    void Start()
    {
        InitConnections();
        //InitState();
    }
    void InitConnections(){
        myRenderer = GetComponent<Renderer>();
    }
    void InitState(){
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DisableColliders();

        myRenderer.materials = new Material[2] { selectedMaterial, myRenderer.materials[1] };
    }
}
