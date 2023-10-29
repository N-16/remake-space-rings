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
    private CrossReward shipCrossReward = CrossReward.NoActivity;

    public override void OnEpisodeBegin(){
        shipBehaviour.Reset();
        //reload source scene (maybe async)
        Destroy(spawnerGameobject);
        spawnerGameobject = Instantiate(spawnerPrefab);
        //cache spawner
        spawner = spawnerGameobject.GetComponent<SourceSpawner>();
        spawner.onShipCross += ShipCrossReward;
        //switch input controller
        //shipInputManager.SetControl(InputController.Agent); 
        //reset ship position, velocity

    }

    public override void CollectObservations(VectorSensor sensor){
        //Debug.Log("Collecting");
        //for now: 
        //add 6 source position (z position as differnce)
        Queue<GameObject> sourceQ = spawner.GetQueue();
        for (int i = 0; i < 3; i++){
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
        //sensor.AddObservation(shipCharge.GetCharge());
    }

    public override void OnActionReceived(ActionBuffers actions){
        //pass input to input manager
        //Debug.Log((actions.DiscreteActions[0] - 1) + "X" + (actions.DiscreteActions[1] - 1));
        shipInputManager.SetAgentInput(actions.DiscreteActions[0]);
        //if z velocty == 0: end episode and punish
        if (shipCharge.GetCharge() == 0f){
            Debug.Log("Reward Deducted");
            AddReward(-100f);
            EndEpisode();
        }
        //reward on crossing source
        if (shipCrossReward != CrossReward.NoActivity){
            if (shipCrossReward == CrossReward.Cross){
                shipCrossReward = CrossReward.NoActivity;
                AddReward(10f);
                Debug.Log("Reward: " + GetCumulativeReward());
            }
            else{
                shipCrossReward = CrossReward.NoActivity;
                //Queue<GameObject> sourceQ = spawner.GetQueue();
                //AddReward(-5f);
                Debug.Log("Reward: " + GetCumulativeReward());
                //EndEpisode();
            }
            
        }
        
        
    }
    public override void Heuristic(in ActionBuffers actionsOut){
        //base.Heuristic(actionsOut);
        //switch input
        var actions = actionsOut.DiscreteActions;
        //Debug.Log("heuristic");
        actions[0] = shipInputManager.AxisToAgent(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    public void ShipCrossReward(bool cross){
        Debug.Log("Event triggered");
        if (cross){
            shipCrossReward = CrossReward.Cross;
        }
        else{
            shipCrossReward = CrossReward.NoCross;
        }
        /*float reward = cross ? 5f : -5f;
        AddReward(reward);
        Debug.Log("added reward " + reward);*/
    }
    public enum CrossReward{
        NoActivity, Cross, NoCross
    }
}
