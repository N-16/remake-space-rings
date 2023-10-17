using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInputManager : MonoBehaviour{
    private float agentHorizontal = 0f, agentVertical= 0f;
    private InputController inputController = InputController.Human;
    
    public (float,float) GetInput(){
        if (inputController == InputController.Human){
            return (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        return (agentHorizontal, agentVertical);
    }

    public void SetAgentInput(float horizontal, float vertical){
        if (inputController == InputController.Human){
            Debug.LogError("Can't Set agent input cuz Input Control is set to human");
        }
        agentHorizontal = horizontal;
        agentVertical = vertical;
    }
    public void SetControl(InputController controller){
        inputController = controller;
    }
}

public enum InputController{
        Human, 
        Agent
}


