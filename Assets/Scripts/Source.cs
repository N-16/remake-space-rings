using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Source : MonoBehaviour{
    Transform ship;
    Transform thisTransform;
    bool coroutineStarted = false;
    void Start(){
        ship = GameObject.FindWithTag("Player").transform;
        thisTransform = transform;
    }

    void Update(){
        if (ship.position.z > transform.position.z && !coroutineStarted){
            coroutineStarted = true;
            StartCoroutine(DisableRoutine(1f));
        }
    }

    IEnumerator DisableRoutine(float time){
        float startTime = Time.time;
        while(Time.time - startTime < time){
            yield return null;
        }
        coroutineStarted = false;
        gameObject.SetActive(false);
    } 
    
}
