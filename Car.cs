using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Car : MonoBehaviour
{
    public Rigidbody rigid;
    public WheelCollider wheel1, wheel2, wheel3, wheel4;
    public float drivespeed, steerspeed;
    float horizontalInput, verticalInput;

    public Player1 driver;
    

    // Update is called once per frame
    void Update()
    {
        
        if (driver != null)
        {
            horizontalInput = driver.GetSteeringInput();
            verticalInput = driver.GetForwardInput();
        }
    }

    void FixedUpdate()
    {
        float motor = verticalInput * drivespeed;
        float steering = horizontalInput * steerspeed;
        wheel1.motorTorque = motor;
        wheel2.motorTorque = motor;
        wheel3.motorTorque = motor;
        wheel4.motorTorque = motor;

        wheel1.steerAngle = steering;
        wheel2.steerAngle = steering;
    }

   
public float GetOrientation()
{
    // Use the Transform's rotation for consistent results
    return transform.eulerAngles.y;
}



    public Vector3 GetPosition()
    {
        return rigid.position;
        
    }

    public 
    float GetSpeed()
    {
        return rigid.velocity.magnitude;
    }

}
