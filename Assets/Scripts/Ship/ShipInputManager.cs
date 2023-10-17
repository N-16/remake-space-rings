using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInputManager : MonoBehaviour{
    private float agentHorizontal = 0f, agentVertical= 0f;
    [SerializeField] private InputController inputController = InputController.Human;
    
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
        if (horizontal != 1 || horizontal != -1 || vertical != 1 || vertical != -1 || horizontal != 0 || vertical != 0){
            Debug.LogError(horizontal + " " + vertical);
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


