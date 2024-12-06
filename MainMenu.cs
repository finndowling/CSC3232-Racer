using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel; // Main Menu Panel
    [SerializeField] private GameObject controlsPanel; // Controls Panel
    [SerializeField] private GameObject highScoresPanel; // High Scores Panel

    private void Start()
    {
        InitializePanels(); // Ensure only the main menu is active at the start
    }

    public void StartGame()
    {
        SceneManager.LoadScene("RacingScene"); // Replace with your gameplay scene name
    }

    public void OpenControls()
    {
        ShowPanel(controlsPanel); // Show the ControlsPanel
    }

    public void CloseControls()
    {
        ShowPanel(mainMenuPanel); // Return to the Main Menu (MainMenuPanel)
    }

    public void OpenHighScores()
    {
        ShowPanel(highScoresPanel); // Show the HighScoresPanel
    }

    public void CloseHighScores()
    {
        ShowPanel(mainMenuPanel); // Return to the Main Menu (MainMenuPanel)
    }

    private void InitializePanels()
    {
        if (mainMenuPanel == null || controlsPanel == null || highScoresPanel == null)
        {
            Debug.LogError("MainMenuPanel, ControlsPanel, or HighScoresPanel is not assigned in the Inspector.");
            return;
        }

        // Activate only the Main Menu panel at the start
        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);
        highScoresPanel.SetActive(false);
    }

    private void ShowPanel(GameObject panelToShow)
    {
        if (panelToShow == null)
        {
            Debug.LogError("Panel to show is null. Ensure it is assigned in the Inspector.");
            return;
        }

        // Deactivate all panels
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(false);
        highScoresPanel.SetActive(false);

        // Activate the requested panel
        panelToShow.SetActive(true);
    }
}
