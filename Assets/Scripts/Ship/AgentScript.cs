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
    private float cacheDistance = 0f;
    [SerializeField] private ScoreUI scoreUI;

    void Start(){
        Debug.unityLogger.logEnabled = false;
    }

    public override void OnEpisodeBegin(){
        shipBehaviour.Reset();
        Destroy(spawnerGameobject);
        spawnerGameobject = Instantiate(spawnerPrefab);
        //cache spawner
        spawner = spawnerGameobject.GetComponent<SourceSpawner>();
        spawner.onShipCross += ShipCrossReward;
    }

    public override void CollectObservations(VectorSensor sensor){

        Queue<GameObject> sourceQ = spawner.GetQueue();
        for (int i = 0; i < 2; i++){
            Vector3 position = sourceQ.ElementAt(i).transform.position;
            position.z -= shipTransform.position.z;
            sensor.AddObservation(position);
        }
        sensor.AddObservation(shipTransform.position.x);
        sensor.AddObservation(shipTransform.position.y);
        sensor.AddObservation(shipRB.velocity);
    }

    public override void OnActionReceived(ActionBuffers actions){

        shipInputManager.SetAgentInput(actions.DiscreteActions[0]);
        if (shipCharge.GetCharge() == 0f){


            if (shipRB.velocity.z < 0.5f){
                AddReward(-10f);
                EndEpisode();
            }
        }
        //reward on crossing source
        if (shipCrossReward != CrossReward.NoActivity){
            if (shipCrossReward == CrossReward.Cross){
                shipCrossReward = CrossReward.NoActivity;
                AddReward(1f);
            }
            else{
                shipCrossReward = CrossReward.NoActivity;
            }
            
        }
        //Add reward if it closer to the upcoming ring
        Transform nextSource = spawner.GetQueue().Peek().transform;
        if (cacheDistance < Vector2.Distance(new Vector2(shipTransform.position.x, shipTransform.position.y),
                                            new Vector2 (nextSource.position.x, nextSource.position.y))){
            AddReward(-0.2f);
        }
        else{
            AddReward(0.1f);
        }
        cacheDistance = Vector2.Distance(new Vector2(shipTransform.position.x, shipTransform.position.y),
                                            new Vector2 (nextSource.position.x, nextSource.position.y));
        scoreUI.SetText("Reward : " + GetCumulativeReward().ToString());
        
    }
    public override void Heuristic(in ActionBuffers actionsOut){
        var actions = actionsOut.DiscreteActions;
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
    }
    public enum CrossReward{
        NoActivity, Cross, NoCross
    }
}
