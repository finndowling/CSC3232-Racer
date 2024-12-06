using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public Car car;
    public Car car1;
    public Car car2;
    public Car car3;

    public Player1 humanPlayer;  // Assign in the Inspector
    public Player1 aiPlayer1;    // Assign in the Inspector
    public Player1 aiPlayer2;    // Assign in the Inspector
    public Player1 aiPlayer3;    // Assign in the Inspector

    private bool isGameOver = false;

    void Start()
    {
        // Assign cars to players
        humanPlayer.AssignCar(car);
        aiPlayer1.AssignCar(car1);
        aiPlayer2.AssignCar(car2);
        aiPlayer3.AssignCar(car3);

        // Register the human player in CheckpointMan
        CheckpointMan.Instance.CarReachedCheckpoint(humanPlayer.gameObject, CheckpointMan.Instance.Checkpoints[0]);

        // Subscribe to lap completion event
        CheckpointMan.OnHighScoresUpdated += CheckLapCompletion;
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }
    }

    public void OnPlayerHitByBlockingAI()
    {
        if (!isGameOver)
        {
            EndGame("Player was hit by a BlockingAI!");
        }
    }

    private void CheckLapCompletion(List<float> lapTimes)
    {
        // Check if the human player's lap times are updated
        if (CheckpointMan.Instance.GetCarLapTimes(humanPlayer.gameObject).Count > 0)
        {
            EndGame("Player completed a lap!");
        }
    }

    private void EndGame(string message)
    {
        isGameOver = true;
        Debug.Log(message);
        // Additional game-over logic, e.g., stop cars, show UI
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        CheckpointMan.OnHighScoresUpdated -= CheckLapCompletion;
    }
}
