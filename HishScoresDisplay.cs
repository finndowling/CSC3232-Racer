using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoresDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoresText; // Assign via Inspector

    private void OnEnable()
{
    if (CheckpointMan.Instance == null)
    {
        Debug.LogError("CheckpointMan.Instance is null. Ensure CheckpointMan is initialized.");
        return;
    }

    if (highScoresText == null)
    {
        Debug.LogError("HighScoresText is not assigned in the Inspector.");
        return;
    }

    UpdateHighScores(CheckpointMan.Instance.GetHighScores());
    CheckpointMan.OnHighScoresUpdated += UpdateHighScores;
}


    private void OnDisable()
    {
        CheckpointMan.OnHighScoresUpdated -= UpdateHighScores;
    }

    public void UpdateHighScores(List<float> highScores)
    {
        highScoresText.text = "Top 10 Lap Times:\n";
        for (int i = 0; i < highScores.Count; i++)
        {
            highScoresText.text += $"{i + 1}. {highScores[i]:F2} seconds\n";
        }
    }
}
