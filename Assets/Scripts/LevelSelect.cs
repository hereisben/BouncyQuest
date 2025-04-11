using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public GameObject levelButtonPrefab; // Prefab for level buttons
    public Transform buttonContainer; // Parent object for level buttons

    void Start()
    {
        int totalLevels = SceneManager.sceneCountInBuildSettings; // Get total number of scenes in build settings

        for (int i = 1; i < totalLevels; i++) // Start from 1 to skip the first scene (usually the main menu)
        {
            GameObject button = Instantiate(levelButtonPrefab, buttonContainer); // Create a new button instance
            TextMeshProUGUI[] texts = button.GetComponentsInChildren<TextMeshProUGUI>(); // Get all TextMeshPro components in the button

            if (texts.Length >= 2) {
                texts[1].text = i.ToString(); // Set the button text to the level number
            }

            int sceneIndex = i; // Capture the current index for the button's onClick event
            button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SceneManager.LoadScene(sceneIndex)); // Add a listener to load the scene when clicked
        }
    }
}
