using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer1 : Player1
{
    private float maxForwardInput;
    private Transform nextWaypoint;
    private bool hasReachedCheckpoint;

    void Start()
    {
        maxForwardInput = UnityEngine.Random.Range(0.5f, 0.8f);

        checkpointManager = FindObjectOfType<CheckpointMan>();
        if (checkpointManager == null || checkpointManager.Checkpoints.Count == 0)
        {
            Debug.LogError("CheckpointManager is missing or has no checkpoints.");
            return;
        }

        currentCheckpointIndex = 0;
        nextWaypoint = checkpointManager.Checkpoints[currentCheckpointIndex];
        Debug.Log($"Starting target waypoint: {nextWaypoint.name}");
    }

    void Update()
    {
        if (nextWaypoint != null)
        {
            DriveTowardsWaypoint(nextWaypoint.position);

            float distanceToWaypoint = Vector3.Distance(myCar.GetPosition(), nextWaypoint.position);
            if (distanceToWaypoint < 5f && !hasReachedCheckpoint) // Adjust threshold as needed
            {
                hasReachedCheckpoint = true;
                UpdateNextWaypoint();
            }
        }
        else
        {
            Debug.LogWarning("Next waypoint is not assigned.");
        }
    }

    private void DriveTowardsWaypoint(Vector3 nextWaypointPosition)
    {
        Vector3 carPos = myCar.GetPosition();
        Vector3 nextWaypointDistance = nextWaypointPosition - carPos;
        float carHeading = myCar.GetOrientation();

        float waypointHeading = Mathf.Atan2(nextWaypointDistance.x, nextWaypointDistance.z) * Mathf.Rad2Deg;
        float turningRequired = Mathf.DeltaAngle(carHeading, waypointHeading);

        steeringInput = Mathf.Clamp(turningRequired / 90f, -1f, 1f);

        if (Mathf.Abs(turningRequired) > 30f)
        {
            forwardInput = Mathf.Lerp(forwardInput, 0.2f, Time.deltaTime * 2);
        }
        else
        {
            forwardInput = Mathf.Lerp(forwardInput, maxForwardInput, Time.deltaTime * 2);
        }
    }

    public void UpdateNextWaypoint()
    {
        if (checkpointManager == null || checkpointManager.Checkpoints.Count == 0)
        {
            Debug.LogError($"{gameObject.name}: CheckpointManager is not properly configured.");
            return;
        }

        currentCheckpointIndex = (currentCheckpointIndex + 1) % checkpointManager.Checkpoints.Count;
        nextWaypoint = checkpointManager.Checkpoints[currentCheckpointIndex];
        hasReachedCheckpoint = false;
        Debug.Log($"{gameObject.name} updated target to: {nextWaypoint.name}");
    }
}
