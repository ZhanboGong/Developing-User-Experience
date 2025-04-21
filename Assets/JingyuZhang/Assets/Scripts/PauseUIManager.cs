using UnityEngine;
using UnityEngine.UI;

public class PauseUIManager : MonoBehaviour
{
    [Header("页面切换")]
    public GameObject pausePage;
    public GameObject settingsPage;
    public GameObject helpPage;

    [Header("音量控制")]
    public Slider volumeSlider;



    // 切换页面（例如从暂停页切换到设置页）
    public void SwitchPage(GameObject targetPage)
    {
        pausePage.SetActive(false);
        settingsPage.SetActive(false);
        helpPage.SetActive(false);
        targetPage.SetActive(true);
    }



    // ------ 按钮事件方法 ------
    public void OnPauseButtonClick()
    {
        Time.timeScale = 0f;
        BGMManager.Instance.PauseBGM(); // 暂停BGM
        pausePage.SetActive(true);
    }

    public void OnResumeButtonClick()
    {
        Time.timeScale = 1f;
        BGMManager.Instance.ResumeBGM(); // 恢复BGM
        pausePage.SetActive(false);
    }
}