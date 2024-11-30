using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public string Name = null;
    public Car myCar = null;
    public float steeringInput;
    public float forwardInput;

    protected CheckpointMan checkpointManager; // Changed to protected
    protected int currentCheckpointIndex;     // Changed to protected

    public void Initialize(string Name)
    {
        this.Name = Name;
    }

    void Start()
    {
        checkpointManager = FindObjectOfType<CheckpointMan>();
        if (checkpointManager == null || checkpointManager.Checkpoints.Count == 0)
        {
            Debug.LogError("CheckpointManager is missing or has no checkpoints.");
            return;
        }

        currentCheckpointIndex = checkpointManager.Checkpoints.Count - 1;
    }

    void Update()
    {
        steeringInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.C))
        {
            ResetToLastCheckpoint();
        }
    }

    public void AssignCar(Car car) // Fixed naming to PascalCase
    {
        this.myCar = car;
        this.myCar.driver = this;
    }

    public float GetForwardInput()
    {
        return forwardInput;
    }

    public float GetSteeringInput()
    {
        return steeringInput;
    }

    private void ResetToLastCheckpoint()
{
    if (checkpointManager == null || checkpointManager.Checkpoints.Count == 0)
    {
        Debug.LogError("CheckpointManager is missing or has no checkpoints.");
        return;
    }

    // Retrieve the correct current checkpoint index from CheckpointMan
    currentCheckpointIndex = checkpointManager.GetCarCheckpointIndex(myCar.gameObject);
    if (currentCheckpointIndex == -1)
    {
        Debug.LogError($"{Name} does not have a valid checkpoint index in CheckpointManager.");
        return;
    }

    // Calculate the last passed checkpoint (currentCheckpointIndex - 1)
    int lastCheckpointIndex = currentCheckpointIndex - 1;
    if (lastCheckpointIndex < 0)
    {
        lastCheckpointIndex = checkpointManager.Checkpoints.Count - 1; // Wrap to the last checkpoint
    }

    // Get the last checkpoint
    Transform lastCheckpoint = checkpointManager.Checkpoints[lastCheckpointIndex];

    // Reset the car's position and rotation
    myCar.transform.position = lastCheckpoint.position;

    Vector3 checkpointRotationEuler = lastCheckpoint.rotation.eulerAngles;
    checkpointRotationEuler.x = 0; // Set rotation.x to 0
    myCar.transform.rotation = Quaternion.Euler(checkpointRotationEuler);

    // Reset the car's velocity and angular velocity
    Rigidbody carRigidbody = myCar.GetComponent<Rigidbody>();
    if (carRigidbody != null)
    {
        carRigidbody.velocity = Vector3.zero;         // Stop all linear movement
        carRigidbody.angularVelocity = Vector3.zero; // Stop all angular movement
    }

    Debug.Log($"{Name} reset to checkpoint {lastCheckpointIndex}: {lastCheckpoint.name}");
}






}
