using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    public CanvasGroup gameOverCanvasGroup; // Reference to the CanvasGroup component for fading
    public float fadeDuration = 1f; // Duration of the fade effect

    public void showGameOverScreen()
    {
        // Activate the Game Over screen UI
        gameObject.SetActive(true);
        StartCoroutine(FadeIn()); // Start the fade-in effect
    }
    
    IEnumerator FadeIn()
    {
        // Fade in the Game Over screen
        float elapsedTime = 0f;
        gameOverCanvasGroup.alpha = 0f; // Start with transparent

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            gameOverCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null; // Wait for the next frame
        }

        gameOverCanvasGroup.alpha = 1f; // Ensure it's fully opaque at the end
    }

    public void RestartGame()
    {
        // Reload the current scene to restart the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        // Quit the application (works in built version, not in editor)
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
        #else
            Application.Quit(); // Quit the application
        #endif
    }
}
