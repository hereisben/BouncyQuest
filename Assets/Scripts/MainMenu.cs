using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Quit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
        #else
            Application.Quit(); // Quit the application
        #endif
    }
}
