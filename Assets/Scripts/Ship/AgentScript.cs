using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
public class AgentScript : Agent{
    [SerializeField] private ShipInputManager shipInputManager;

    [SerializeField] private ShipBehaviour shipBehaviour;
    [SerializeField] private SceneLoader sceneLoader;
    public override void OnEpisodeBegin(){
        //reload source scene (maybe async)
        bool sceneOperationCompleted = false;
        sceneLoader.UnloadScene("GameView", () => sceneOperationCompleted = true);
        while (!sceneOperationCompleted){

        }
        sceneOperationCompleted = false;
        sceneLoader.LoadScene("GameView", () => sceneOperationCompleted = true);
        while (!sceneOperationCompleted){

        }
        //switch input controller
        shipInputManager.SetControl(InputController.Agent); 
        //reset ship position, velocity
        shipBehaviour.Reset();
    }

    public override void CollectObservations(VectorSensor sensor){
        base.CollectObservations(sensor);
        //for now: 
        //add 6 source position (z position as differnce)
        //ship position (except z position)
        //ship velocity
        //charge
    }

    public override void OnActionReceived(ActionBuffers actions){
        base.OnActionReceived(actions);
        //pass input to input manager
        //if z velocty == 0: end episode and punish
        //else add some minimal reward maybe
    }
    public override void Heuristic(in ActionBuffers actionsOut){
        base.Heuristic(actionsOut);
        //switch input
    }
}
