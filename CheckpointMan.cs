using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMan : MonoBehaviour
{
    [SerializeField] private List<Transform> checkpoints; // Assign via Inspector
    [SerializeField] private float checkpointProximityThreshold = 5f;

    private Dictionary<GameObject, int> carCheckpointIndex = new Dictionary<GameObject, int>();

    public List<Transform> Checkpoints => checkpoints; // Read-only access

    public static event Action<GameObject, Transform> OnCheckpointReached;

    private void Start()
    {
        if (checkpoints == null || checkpoints.Count == 0)
        {
            Debug.LogError("Checkpoints list is empty or not assigned.");
            return;
        }

        for (int i = 0; i < checkpoints.Count; i++)
        {
            Debug.Log($"Checkpoint {i}: {checkpoints[i].name}");
        }
    }

    public void CarReachedCheckpoint(GameObject car, Transform checkpoint)
    {
        if (!carCheckpointIndex.ContainsKey(car))
        {
            carCheckpointIndex[car] = 0; // Start at the first checkpoint
        }

        int currentCheckpointIndex = carCheckpointIndex[car];
        Transform expectedCheckpoint = checkpoints[currentCheckpointIndex];

        if (expectedCheckpoint == checkpoint)
        {
            Debug.Log($"{car.name} reached checkpoint {currentCheckpointIndex}: {checkpoint.name}");

            // Update the checkpoint index to the next checkpoint
            currentCheckpointIndex++;
            if (currentCheckpointIndex >= checkpoints.Count)
            {
                currentCheckpointIndex = 0; // Wrap to the first checkpoint
            }
            carCheckpointIndex[car] = currentCheckpointIndex; // Save the updated index

            // Notify AI cars to update their target waypoint
            AIPlayer1 carAI = car.GetComponent<AIPlayer1>();
            if (carAI != null)
            {
                carAI.UpdateNextWaypoint();
                Debug.Log($"{car.name} notified to update its target checkpoint.");
            }
        }
        else
        {
            Debug.LogWarning($"{car.name} hit the wrong checkpoint. Expected: {expectedCheckpoint.name}, but hit: {checkpoint.name}");
        }
    }

    // Public method to get the current checkpoint index for a specific car
    public int GetCarCheckpointIndex(GameObject car)
    {
        if (carCheckpointIndex.ContainsKey(car))
        {
            return carCheckpointIndex[car];
        }
        else
        {
            Debug.LogWarning($"{car.name} does not have a checkpoint index set.");
            return -1; // Return -1 if the car is not tracked
        }
    }
}
