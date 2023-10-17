using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollision : MonoBehaviour{

    [SerializeField] private Collider col;
    [SerializeField] private float idleCollisionForce = 3f;//when x and z direction velocity of the ship is super low
    private Transform myTransform;

    void Start(){
        myTransform = transform;
    }
    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Player"){
            Rigidbody shipRB = collision.rigidbody;
            Vector3 force;
            if (Math.Abs(shipRB.velocity.x) + Math.Abs(shipRB.velocity.y) < 7f){
                force = Vector3.Scale(shipRB.position - myTransform.position, new Vector3(1f, 1f, 0f)).normalized * idleCollisionForce;
                Debug.Log("ZEROO");
            }
            else{
                force = new Vector3(-shipRB.velocity.x + 0.1f, -shipRB.velocity.y + 0.1f, 0f);
            }
            collision.rigidbody.AddForce(force, ForceMode.Impulse);
            StartCoroutine(DisableRoutine(0.1f));
        }
    }

    IEnumerator DisableRoutine(float time){
        col.enabled = false;
        float startTime = Time.time;
        while(Time.time - startTime < time){
            yield return null;
        }
        col.enabled = true;
    } 
}
