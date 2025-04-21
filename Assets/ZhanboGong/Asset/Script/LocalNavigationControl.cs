using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalNavigationControl : MonoBehaviour
{
    // WorkShop
    private int count;
    public GameObject countTextObject;
    public TextMeshProUGUI countText;
    public AudioSource clickAudio;
    public AudioSource Score2;
    public AudioSource Score5;
    public AudioSource BackgroundMusic;
    //
    public AudioSource crashAudio;
    public GameObject popup;
    public GameObject PauseButton;
    public TextMeshProUGUI TimeText;
    public GameObject TimeTextObject;
    public GameObject FieldPanel;
    //
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI timeUsedText;
    float clearTime;
    // Win and Lose settlement
    public AudioSource WinAudio;
    public AudioSource LoseAudio;
    public TextMeshProUGUI WinScoreText;
    public TextMeshProUGUI WinTimeText;
    public TextMeshProUGUI LoseScoreText;
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        clearTime += Time.fixedDeltaTime;
        if (TimeText.text == "00:00:00")
        {

            FieldPagePopsUp();
        }
        SetScoreText();
        SetTimeText();
        SetWinLosePage();
    }

    //Workshop
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            clickAudio.Play();
        }
        else if (other.gameObject.CompareTag("pickup2"))
        {
            other.gameObject.SetActive(false);
            count = count + 2;
            SetCountText();
            Score2.Play();
        }
        else if (other.gameObject.CompareTag("pickup3"))
        {
            other.gameObject.SetActive(false);
            count = count + 5;
            SetCountText();
            Score5.Play();
        }
        else if (other.gameObject.CompareTag("trophy"))
        {
            TimeTextObject.SetActive(false);
            PauseButton.SetActive(false);
            other.gameObject.SetActive(false);
            Time.timeScale = 0f;
            countTextObject.SetActive(false);
            BackgroundMusic.Stop();
            SettlementPagePopsUp();
            WinAudio.Play();
        }
        else if (other.gameObject.CompareTag("barrier"))
        {
            crashAudio.Play();
        }
    }

    public void SetCountText()
    {
        countText.text = "Score  " + count.ToString();
    }

    public void SetScoreText()
    {
        currentScoreText.text = "Current Score: " + count.ToString();
    }

    public void SetWinLosePage()
    {
        WinScoreText.text = LoseScoreText.text = "Total Points: " + count.ToString() + "/36";
        WinTimeText.text = "Time Used: " + TimeSpan.FromSeconds(value: clearTime).ToString(format: @"mm\:ss\:ff");
    }

    public void SetTimeText()
    {
        timeUsedText.text = "Time Used: " + TimeSpan.FromSeconds(value: clearTime).ToString(format: @"mm\:ss\:ff");
    }

    public void SettlementPagePopsUp()
    {
        if (popup != null)
        {
            popup.SetActive(true);
        }
    }

    public void FieldPagePopsUp()
    {
        if (FieldPanel != null)
        {
            TimeTextObject.SetActive(false);
            PauseButton.SetActive(false);
            Time.timeScale = 0f;
            FieldPanel.SetActive(true);
            countTextObject.SetActive(false);
            BackgroundMusic.Stop();
            LoseAudio.Play();
        }
    }

    
}
