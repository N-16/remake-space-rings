using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Rigidbody ship;//The ship
    private float fwdSpeed = 250f;//Forward speed
    private float steerForce = 7f//Force Component to steer
                                ,rotResetSpeed = 2f;//AI Reset Speed
    [SerializeField] private ShipInputManager shipInputManager;
    private float actualSteerForce;
    private float defaultSteerForce = 7f;
    private float actualSpeed;
    private float defaultFwdSpeed = 250f;
     
    void Start(){
        ship = GetComponent<Rigidbody>();
    
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
    public float GetActualFwdSpeed(){
        return actualSpeed;
    }
    public float GetFwdSpeed(){
        return fwdSpeed;
    }
    public void SetActualFwdSpeed(float speed){
        actualSpeed = speed;
    }
}
