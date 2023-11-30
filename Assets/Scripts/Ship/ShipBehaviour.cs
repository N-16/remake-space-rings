using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour
{
    [SerializeField] private ShipMovement movementController;
    [SerializeField] private ShipChargeController chargeController;
    [SerializeField] private Rigidbody shipRB;

    [SerializeField] private Transform shipTransform;
    [SerializeField] private SourceSpawner spawner;
    [SerializeField] ScoreUI scoreui;
    private Vector3 startPosition;

    void Start(){
        startPosition = shipTransform.position;
    }
    public void Shutdown(){
        Debug.Log("Shutdown called");
        movementController.SetSteerForce(0f);
        movementController.SetFwdSpeed(0f);
    }

    public void PowerOn(){
        movementController.ResetFwdSpeed();
        movementController.ResetSteerForce();
    }

    public void Reset(){
        //reset velocities
        shipRB.velocity = Vector3.zero;
        movementController.SetActualFwdSpeed(0f);
        shipRB.angularVelocity = Vector3.zero;

        //reset rotation
        shipTransform.eulerAngles = Vector3.zero;

        //reset position
        shipTransform.position = startPosition;
        PowerOn();
        chargeController.Reset();
    }
    
}
