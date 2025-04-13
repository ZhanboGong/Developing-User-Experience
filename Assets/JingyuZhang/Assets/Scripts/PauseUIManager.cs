using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseUIManager : MonoBehaviour
{
    public GameObject pausePage;
    public GameObject settingsPage;
    public GameObject helpPage;
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = GameManager.Instance.GetSavedVolume();
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SwitchPage(GameObject targetPage)
    {
        pausePage.SetActive(false);
        settingsPage.SetActive(false);
        helpPage.SetActive(false);
        targetPage.SetActive(true);
    }

    private void SetVolume(float value) => GameManager.Instance.SetVolume(value);
}