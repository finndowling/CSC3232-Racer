using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMan : MonoBehaviour
{
    [SerializeField] private List<Transform> checkpoints; // Assign via Inspector
    [SerializeField] private float checkpointProximityThreshold = 5f;

    private Dictionary<GameObject, int> carCheckpointIndex = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, List<float>> carLapTimes = new Dictionary<GameObject, List<float>>();
    private Dictionary<GameObject, float> carLapStartTime = new Dictionary<GameObject, float>();

    private List<float> topTenLapTimes = new List<float>(); // Stores the top 10 fastest lap times

    public static CheckpointMan Instance { get; private set; } // Singleton instance

    public List<Transform> Checkpoints => checkpoints; // Read-only access

    public static event Action<List<float>> OnHighScoresUpdated;

    private GameObject humanPlayer; // Reference to the human player

    private void Awake()
    {
        // Ensure only one instance of the singleton exists
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple instances of CheckpointMan detected. Destroying the duplicate.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

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

        // Find the human player
        humanPlayer = GameObject.FindWithTag("Player");

        // Initially deactivate all checkpoint banners
        foreach (Transform checkpoint in checkpoints)
        {
            SetBannerVisibility(checkpoint, false);
        }

        // Activate the banner for the first checkpoint
        if (checkpoints.Count > 0)
        {
            SetBannerVisibility(checkpoints[0], true);
        }
    }

    public void CarReachedCheckpoint(GameObject car, Transform checkpoint)
    {
        if (!carCheckpointIndex.ContainsKey(car))
        {
            carCheckpointIndex[car] = 0; // Start at the first checkpoint
            carLapStartTime[car] = Time.time; // Record the start time of the first lap
            carLapTimes[car] = new List<float>(); // Initialize lap times list
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
                RecordLapTime(car); // Record lap time when completing a lap
            }
            carCheckpointIndex[car] = currentCheckpointIndex; // Save the updated index

            // Handle banner visibility for the human player
            if (car == humanPlayer)
            {
                UpdateCheckpointBanners(currentCheckpointIndex);
            }

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

    // Handle visibility of checkpoint banners for the human player
    private void UpdateCheckpointBanners(int currentCheckpointIndex)
    {
        // Deactivate all banners
        foreach (Transform checkpoint in checkpoints)
        {
            SetBannerVisibility(checkpoint, false);
        }

        // Activate the banner for the current checkpoint
        if (currentCheckpointIndex < checkpoints.Count)
        {
            SetBannerVisibility(checkpoints[currentCheckpointIndex], true);
        }
    }

    // Helper method to show or hide the banner for a checkpoint
    private void SetBannerVisibility(Transform checkpoint, bool isVisible)
    {
        Transform banner = checkpoint.Find("Plane (1)");
        if (banner != null)
        {
            banner.gameObject.SetActive(isVisible);
        }
    }

    // Record the lap time for the car
    private void RecordLapTime(GameObject car)
    {
        if (!carLapStartTime.ContainsKey(car))
        {
            Debug.LogWarning($"No lap start time recorded for {car.name}.");
            return;
        }

        float lapTime = Time.time - carLapStartTime[car];
        carLapTimes[car].Add(lapTime); // Add the lap time to the car's lap times list
        carLapStartTime[car] = Time.time; // Reset the lap start time for the next lap

        Debug.Log($"{car.name} completed a lap in {lapTime:F2} seconds.");

        UpdateHighScores(lapTime);
    }

    // Update the high scores list
    private void UpdateHighScores(float lapTime)
    {
        topTenLapTimes.Add(lapTime);
        topTenLapTimes.Sort(); // Sort in ascending order
        if (topTenLapTimes.Count > 10)
        {
            topTenLapTimes.RemoveAt(10); // Keep only the top 10 times
        }

        // Notify any listeners (e.g., high scores page) of the updated high scores
        OnHighScoresUpdated?.Invoke(topTenLapTimes);
    }

    // Public method to get the lap times for a specific car
    public List<float> GetCarLapTimes(GameObject car)
    {
        if (carLapTimes.ContainsKey(car))
        {
            return carLapTimes[car];
        }
        else
        {
            Debug.LogWarning($"{car.name} does not have any recorded lap times.");
            return new List<float>(); // Return an empty list if no lap times exist
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

    // Public method to retrieve the top ten lap times
    public List<float> GetHighScores()
    {
        return new List<float>(topTenLapTimes); // Return a copy of the list to avoid unintended modifications
    }
}
