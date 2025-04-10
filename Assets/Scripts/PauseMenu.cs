using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) // Check if the Escape key is pressed
        {
            if (pauseMenuUI.activeSelf) {
                Resume(); // If the menu is active, resume the game
            } else {
                Pause(); // If the menu is not active, pause the game
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false); // Hide the pause menu UI
        Time.timeScale = 1f; // Resume the game time
    }

    public void Pause() {
        pauseMenuUI.SetActive(true); // Show the pause menu UI
        Time.timeScale = 0f; // Pause the game time
    }

    public void Quit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
        #else
            Application.Quit(); // Quit the application
        #endif
    }
}
