using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject mutedIcon;
    public GameObject unmutedIcon;

    private bool isMuted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isMuted = AudioListener.volume == 0f;
        UpdateIcons();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute();

        }
    }

    void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0f : 1f;
        UpdateIcons();
    }

    void UpdateIcons()
    {
        if (mutedIcon != null) mutedIcon.SetActive(isMuted);
        if (unmutedIcon != null) unmutedIcon.SetActive(!isMuted);
    }
}
