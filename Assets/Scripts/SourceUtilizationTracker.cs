using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoruceUtilizationTracker : MonoBehaviour{
    private bool utilized = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MarkUtilize(){
        utilized = true;
    }
    public void OnDisable(){
        utilized = false;
    }
    public bool GetUtilization(){
        return utilized;
    }
}
