using UnityEngine;

public class HowToPlayCanvas : MonoBehaviour
{
    public GameObject howToPlayCanvas;
    public GameObject mainMenuCanvas;

    public void ShowMainMenu()
    {
        howToPlayCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
