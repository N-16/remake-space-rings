using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Linq;
public class AgentScript : Agent{
    [SerializeField] private ShipInputManager shipInputManager;
    [SerializeField] private ShipBehaviour shipBehaviour;
    [SerializeField] private Transform shipTransform;
    [SerializeField] private Rigidbody shipRB;
    [SerializeField] private ShipChargeController shipCharge;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private ShipMovement shipMovement;
    [SerializeField] private GameObject spawnerPrefab;
    [SerializeField] private GameObject spawnerGameobject;
    private SourceSpawner spawner;
    public override void OnEpisodeBegin(){
        Debug.Log("ON EPISODE BEGIN");
        //reload source scene (maybe async)
        Destroy(spawnerGameobject);
        spawnerGameobject = Instantiate(spawnerPrefab);
        //cache spawner
        spawner = spawnerGameobject.GetComponent<SourceSpawner>();
        //switch input controller
        //shipInputManager.SetControl(InputController.Agent); 
        //reset ship position, velocity
        shipBehaviour.Reset();
    }

    public override void CollectObservations(VectorSensor sensor){
        Debug.Log("Collecting");
        //for now: 
        //add 6 source position (z position as differnce)
        Queue<GameObject> sourceQ = spawner.GetQueue();
        for (int i = 0; i < 6; i++){
            if (sourceQ.Count < i + 1){
                Debug.LogError(sourceQ.Count);
            }
            Vector3 position = sourceQ.ElementAt(i).transform.position;
            position.z -= shipTransform.position.z;
            sensor.AddObservation(position);
        }
        //ship position (except z position)
        sensor.AddObservation(shipTransform.position.x);
        sensor.AddObservation(shipTransform.position.y);
        //ship velocity
        sensor.AddObservation(shipRB.velocity);
        //charge
        sensor.AddObservation(shipCharge.GetCharge());
    }

    public override void OnActionReceived(ActionBuffers actions){
        //pass input to input manager
        Debug.Log((actions.DiscreteActions[0] - 1) + "X" + (actions.DiscreteActions[1] - 1));
        shipInputManager.SetAgentInput(actions.DiscreteActions[0] - 1, actions.DiscreteActions[1] - 1);
        //if z velocty == 0: end episode and punish
        if (shipMovement.GetActualFwdSpeed() < 0.9f && shipMovement.GetFwdSpeed() == 0f){
            AddReward(-10f);
            EndEpisode();
        }
        //else add some minimal reward maybe
        AddReward(0.001f);
    }
    public override void Heuristic(in ActionBuffers actionsOut){
        //base.Heuristic(actionsOut);
        //switch input
        var actions = actionsOut.DiscreteActions;
        Debug.Log("heuristic");
        actions[0] = (int)Input.GetAxisRaw("Horizontal") + 1;
        actions[1] = (int)Input.GetAxisRaw("Vertical") + 1;
    }
}
