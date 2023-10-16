using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBar : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private ShipChargeController chargeController;

    
    void Update()
    {
        rect.localScale = new Vector2(chargeController.GetCharge()/ 100f,2f); 
    }
}

