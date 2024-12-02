using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Rigidbody rigid;
    public WheelCollider frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;
    public float drivespeed, steerspeed;
    private float horizontalInput, verticalInput;

    public Player1 driver;

    void Start()
    {
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }

        AdjustWheelFriction();
    }

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

        frontLeftWheel.motorTorque = motor;
        frontRightWheel.motorTorque = motor;

        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;
    }

    // Adjust the wheel friction settings for better control
    private void AdjustWheelFriction()
    {
        // Forward friction settings
        WheelFrictionCurve forwardFriction = frontLeftWheel.forwardFriction;
        forwardFriction.stiffness = 2.0f; // Increase stiffness for better grip
        frontLeftWheel.forwardFriction = forwardFriction;
        frontRightWheel.forwardFriction = forwardFriction;
        rearLeftWheel.forwardFriction = forwardFriction;
        rearRightWheel.forwardFriction = forwardFriction;

        // Sideways friction settings
        WheelFrictionCurve sidewaysFriction = frontLeftWheel.sidewaysFriction;
        sidewaysFriction.stiffness = 2.5f; // Adjust for improved turning
        frontLeftWheel.sidewaysFriction = sidewaysFriction;
        frontRightWheel.sidewaysFriction = sidewaysFriction;
        rearLeftWheel.sidewaysFriction = sidewaysFriction;
        rearRightWheel.sidewaysFriction = sidewaysFriction;

        Debug.Log("Wheel friction adjusted for better control.");
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

    public float GetSpeed()
    {
        return rigid.velocity.magnitude;
    }
}
