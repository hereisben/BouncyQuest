using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour
{
    public CanvasGroup winCanvasGroup; // Reference to the CanvasGroup component for fading
    public float fadeDuration = 1f; // Duration of the fade effect

    public GameObject nextLevelButton; // Reference to the Next Level button
    public GameObject restartButton; // Reference to the Restart button

    public void Show() {
        // Activate the Win screen UI
        gameObject.SetActive(true);
        StartCoroutine(FadeIn()); // Start the fade-in effect

        int currentIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int totalScenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

        if (currentIndex == totalScenes - 1) {
            // If it's the last level, show the restart button instead of next level
            nextLevelButton.SetActive(false);
            restartButton.SetActive(true);
        } else {
            // Show the next level button
            nextLevelButton.SetActive(true);
            restartButton.SetActive(false);
        }
    }

    IEnumerator FadeIn() {
        float elapsedTime = 0f;
        winCanvasGroup.alpha = 0f; // Start with transparent
        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            winCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null; // Wait for the next frame
        }
        winCanvasGroup.alpha = 1f; // Ensure it's fully opaque at the end
    }

    public void NextLevel() {
        int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        } else {
            Debug.Log("No more levels!");
        }
    }

    public void RestartLevel() {
        // Reload the current scene to restart the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ShowMainMenu() {
        // Load the main menu scene (assuming it's the first scene in the build settings)
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Quit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
        #else
            Application.Quit(); // Quit the application
        #endif
    }
}
