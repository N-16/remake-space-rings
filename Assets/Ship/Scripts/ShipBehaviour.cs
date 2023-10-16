using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour
{
    [SerializeField] private ShipMovement movementController;
    [SerializeField] private ShipChargeController chargeController;

    public void Shutdown(){
        movementController.SetSteerForce(0f);
        movementController.SetFwdSpeed(0f);
    }

    public void PowerOn(){
        movementController.ResetFwdSpeed();
        movementController.ResetSteerForce();
    }
}
