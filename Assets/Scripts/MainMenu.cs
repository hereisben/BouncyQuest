using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject howToPlayCanvas;
    public GameObject mainMenuCanvas;

    public void ShowHowToPlay()
    {
        howToPlayCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); 
#endif
    }
}
