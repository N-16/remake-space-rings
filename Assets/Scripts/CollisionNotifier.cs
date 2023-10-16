using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionNotifier : MonoBehaviour{
    public UnityEvent onShipPass;
    // Start is called before the first frame update
    void OntriggerExit(Collider other){
        if (other.gameObject.tag == "Player"){
            onShipPass?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
       
}
