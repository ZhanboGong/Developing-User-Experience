using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PausePanel;
    public Button PauseButton;
    public GameObject PauseButtonObject;
    public Button ResumeButton;
    public AudioSource PauseAudio;


    void Start()
    {
        PauseButton.onClick.AddListener(Pause);
        ResumeButton.onClick.AddListener(Resume);
    }
    // Update is called once per frame
    void Update()
    {


    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {

    }

}
