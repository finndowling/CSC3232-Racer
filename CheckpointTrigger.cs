using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private CheckpointMan checkpointManager;

    void Start()
    {
        checkpointManager = FindObjectOfType<CheckpointMan>();
        if (checkpointManager == null)
        {
            Debug.LogError("CheckpointManager not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject car = other.attachedRigidbody?.gameObject ?? other.transform.root.gameObject;

        if (car.CompareTag("Player"))
        {
            checkpointManager?.CarReachedCheckpoint(car, transform);
        }
    }
}
