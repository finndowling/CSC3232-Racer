using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlsPanel; // Assign the ControlsPanel GameObject
    [SerializeField] private GameObject highScoresPanel; // Assign the HighScoresPanel GameObject
    [SerializeField] private Canvas mainMenuCanvas; // Assign the Canvas itself

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
        ShowPanel(mainMenuCanvas.gameObject); // Return to the Main Menu (Canvas)
    }

    public void OpenHighScores()
    {
        ShowPanel(highScoresPanel); // Show the HighScoresPanel
    }

    public void CloseHighScores()
    {
        ShowPanel(mainMenuCanvas.gameObject); // Return to the Main Menu (Canvas)
    }

    private void InitializePanels()
    {
        if (mainMenuCanvas == null || controlsPanel == null || highScoresPanel == null)
        {
            Debug.LogError("MainMenuCanvas, ControlsPanel, or HighScoresPanel is not assigned in the Inspector.");
            return;
        }

        // Activate only the Main Menu at the start
        mainMenuCanvas.gameObject.SetActive(true);
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

        // Disable all panels first
        mainMenuCanvas.gameObject.SetActive(false);
        controlsPanel.SetActive(false);
        highScoresPanel.SetActive(false);

        // Enable the selected panel
        panelToShow.SetActive(true);

        
    }
}
