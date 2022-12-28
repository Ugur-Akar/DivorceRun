using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GateParent : MonoBehaviour
{
    //Settings

    // Connections
   
    Gate[] gates;
  
    // State Variables
    
    // Start is called before the first frame update
    void Start()
    {
        InitConnections();
        //InitState();
    }
    void InitConnections(){
        gates = GetComponentsInChildren<Gate>();

        foreach (Gate gate in gates)
        {
            gate.DisableColliders += DisableColliders;
        }
    }
    void InitState(){
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DisableColliders()
    {
        foreach(Gate gate in gates)
        {
            gate.GetComponent<Collider>().enabled = false;
        }
    }
}
