using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour
{
    [SerializeField] private ShipMovement movementController;
    [SerializeField] private ShipChargeController chargeController;
    [SerializeField] private Rigidbody shipRB;

    [SerializeField] private Transform shipTransform;
    private Vector3 startPosition;

    void Start(){
        startPosition = shipTransform.position;
    }
    public void Shutdown(){
        movementController.SetSteerForce(0f);
        movementController.SetFwdSpeed(0f);
    }

    public void PowerOn(){
        movementController.ResetFwdSpeed();
        movementController.ResetSteerForce();
    }

    public void Reset(){
        //reset velocities
        shipRB.velocity = new Vector3(0f,0f,0f); 
        shipRB.angularVelocity = new Vector3(0f,0f,0f);

        //reset rotation
        shipTransform.eulerAngles = new Vector3(0f,0f,0f);

        //reset position
        shipTransform.position = startPosition;
    }
}
