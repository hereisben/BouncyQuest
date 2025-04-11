using UnityEngine;
using TMPro; 

public class LevelButton : MonoBehaviour
{
    public TextMeshProUGUI levelText; // Reference to the TextMeshProUGUI component for displaying level number
    public int sceneIndex; // Index of the scene to load

    public void SetData(int index)
    {
        sceneIndex = index; // Set the scene index
        levelText.text = "" + (index + 1); // Update the text to display the level number     
    }
}
