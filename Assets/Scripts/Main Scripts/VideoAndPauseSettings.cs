using UnityEngine;
using UnityEngine.UI;

public class VideoAndPauseSettings : MonoBehaviour
{
    [SerializeField]
    GameObject PausePanel;

    [SerializeField]
    Button UnpauseButton;

    #if UNITY_EDITOR
        [SerializeField]
        bool StopAutoPause = false;
    #endif
    void Awake()
    {
        UnpauseButton.onClick.AddListener(Unpause);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public void ChangeVolume(Slider volume) =>
        AudioListener.volume = volume.value;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) Pause();
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    void Unpause()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    void OnApplicationFocus(bool hasFocus)
    {   
        #if UNITY_EDITOR
            if(!hasFocus && !StopAutoPause) Pause();
        #else
            if(!hasFocus) Pause();
        #endif
    }
}
