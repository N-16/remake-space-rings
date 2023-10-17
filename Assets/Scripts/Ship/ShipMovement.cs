using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Rigidbody ship;//The ship
    [SerializeField] private float fwdSpeed = 350f;//Forward speed
    [SerializeField] private float steerForce = 2.5f//Force Component to steer
                                ,rotResetSpeed = 2f;//AI Reset Speed
    [SerializeField] private ShipInputManager shipInputManager;
    private float actualSteerForce;
    private float defaultSteerForce;
    private float actualSpeed;
    private float defaultFwdSpeed;
     
    void Start(){
        ship = GetComponent<Rigidbody>();
        defaultSteerForce = steerForce; 
        defaultFwdSpeed = fwdSpeed;
    }


    void Update(){
        
    }

    void FixedUpdate(){
        actualSpeed = Mathf.Lerp(actualSpeed, fwdSpeed, Time.fixedDeltaTime);
        actualSteerForce = Mathf.Lerp(actualSteerForce, steerForce, Time.fixedDeltaTime);
        ship.velocity = new Vector3(ship.velocity.x, ship.velocity.y, actualSpeed);
        SteerShip(getInput());//Steer Shipe left,right,up,down,diagonal etc.
    }

    Vector2 getInput(){
        return new Vector2(shipInputManager.GetInput().Item1, shipInputManager.GetInput().Item2);
    }

    void SteerShip(Vector2 steerInput)
    {
        ship.AddForce(new Vector3(steerInput.x * actualSteerForce, -steerInput.y * actualSteerForce, 0f), ForceMode.VelocityChange);
        //ship.velocity = new Vector3(steerInput.x * defaultSteerForce, -steerInput.y * defaultSteerForce, fwdSpeed);
        float tiltAngle = -steerInput.x * 20f;//where to tilt according to direction
        ship.rotation = Quaternion.Slerp(ship.rotation, Quaternion.Euler(0f, 0f, tiltAngle), Time.fixedDeltaTime * rotResetSpeed);
    }

    public void SetSteerForce(float force){
        steerForce = force;
    }

    public void ResetSteerForce(){
        steerForce = defaultSteerForce;
    }

    public void SetFwdSpeed(float speed){
        fwdSpeed = speed;
    }
    public void ResetFwdSpeed(){
        fwdSpeed = defaultFwdSpeed;
    }
    public float GetFwdSpeed(){
        return actualSpeed;
    }
}
