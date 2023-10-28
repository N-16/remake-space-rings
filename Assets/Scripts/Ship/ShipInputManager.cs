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

    public void SetAgentInput(int action){
        switch(action){
            case 0:
                //no input 
                agentHorizontal = 0f;
                agentVertical = 0f;
                break;
            case 1:
                //left 
                agentHorizontal = -1f;
                agentVertical = 0f;
                break;
            case 2:
                //right
                agentHorizontal = 1f;
                agentVertical = 0f;
                break;
            case 3:
                //up
                agentHorizontal = 0f;
                agentVertical = -1f;
                break;
            case 4:
                //down
                agentHorizontal = 0f;
                agentVertical = 1f;
                break;
            case 5:
                //left-up 
                agentHorizontal = -1f;
                agentVertical = -1f;
                break;
            case 6:
                //left-down
                agentHorizontal = -1f;
                agentVertical = 1f;
                break;
            case 7:
                //right-up
                agentHorizontal = 1f;
                agentVertical = -1f;
                break;
            case 8:
                //right-down
                agentHorizontal = 1f;
                agentVertical = 1f;
                break;
        }
    }
    public void SetControl(InputController controller){
        inputController = controller;
    }
    List<(float,float)> axisInput = new List<(float,float)> {(0f, 0f), (-1f, 0f), (1f, 0f), (0f, -1f), (0f, 1f), (-1f, -1f), (-1f, 1f), (1f, -1f), (1f,1f)};
    public (float, float) AgentToAxis(int agentInput){
        if (agentInput < 0 || agentInput > 8){
            Debug.LogError("invalid agent input");
        }
        return axisInput[agentInput];
    }
    public int AxisToAgent(float horizontal, float vertical){
        return axisInput.IndexOf((horizontal, vertical));
    }
}

public enum InputController{
        Human, 
        Agent
}




