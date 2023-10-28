using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipChargeController : MonoBehaviour
{
    [SerializeField] private LayerMask sourceLayer;
    [SerializeField] private float chargeUtilSpeed = 35f;
    [SerializeField] private float rechargeAmount = 70f;
    private float charge = 100f;
    [SerializeField] private UnityEvent onDischarge;
    [SerializeField] private UnityEvent onPowerOn;
    //[SerializeField] private UnityEvent onShipCrossSource;
    bool discharged = false;

    void OnTriggerEnter(Collider other){
        if ((sourceLayer & (1 << other.gameObject.layer)) != 0){
            Recharge();
            //onShipCrossSource?.Invoke();
            other.gameObject.GetComponent<SoruceUtilizationTracker>().MarkUtilize();
        }
    }

    void Update(){
        charge = Math.Max(0f, charge - chargeUtilSpeed * Time.deltaTime);
        if (charge == 0f && !discharged){
            discharged = true;
            onDischarge?.Invoke();
        }
    }

    void Recharge(){
        if (discharged){
            discharged = false;
            onPowerOn?.Invoke();
        }
        charge = Math.Min(100f, charge + rechargeAmount);
    }

    public float GetCharge(){
        return charge;
    }
    public void FullRecharge(){
        charge = 100f;
    }
    public void Reset(){
        FullRecharge();
        discharged = false;
    }
}
