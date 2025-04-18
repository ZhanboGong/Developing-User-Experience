using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 需要添加这个命名空间以重载场景

public class PauseManager1 : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pausePanel;    // 暂停面板
    public Button pauseButton;      // 暂停按钮（游戏内触发暂停）
    public Button resumeButton;     // 继续按钮
    public Button restartButton;    // 新添加的重启按钮

    private bool isPaused = false;

    void Start()
    {
        // 绑定按钮事件
        if (pauseButton != null)
            pauseButton.onClick.AddListener(TogglePause);

        if (resumeButton != null)
            resumeButton.onClick.AddListener(TogglePause);

        // 绑定重启按钮事件
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        // 初始化状态
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    // 暂停/继续切换
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        if (pausePanel != null)
            pausePanel.SetActive(isPaused);
    }

    // 新添加的方法：重启游戏
    public void RestartGame()
    {
        // 恢复时间流速（重要！否则新场景会继续暂停）
        Time.timeScale = 1;
        
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}