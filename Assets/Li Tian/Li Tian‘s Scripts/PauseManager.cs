using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[DisallowMultipleComponent]
public class PauseManager : MonoBehaviour
{
    // UI 引用
    [Header("UI References")]
    [Tooltip("暂停时显示的面板")]
    public GameObject pausePanel;

    [Tooltip("触发暂停的按钮")]
    public Button pauseButton;

    [Tooltip("继续游戏的按钮")]
    public Button resumeButton;

    // 音频设置
    [Header("Audio Settings")]
    [Tooltip("主音频混合器（可选）")]
    public AudioMixer mainMixer;

    [Tooltip("暂停时是否静音")]
    public bool muteOnPause = true;

    // 私有变量
    private bool _isPaused;
    private float _prePauseVolume;

    void Start()
    {
        // 初始化状态
        ForceUnpause();

        // 绑定按钮事件（使用安全绑定方式）
        SafeButtonBind(pauseButton, OnPauseButtonClick);
        SafeButtonBind(resumeButton, OnResumeButtonClick);
    }

    // 安全绑定按钮（防止空引用）
    private void SafeButtonBind(Button button, UnityEngine.Events.UnityAction action)
    {
        if (button == null)
        {
            Debug.LogError("按钮未分配！", this);
            return;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
        button.onClick.AddListener(() => Debug.Log($"按钮 {button.name} 被点击"));
    }

    // 暂停按钮点击
    private void OnPauseButtonClick()
    {
        if (_isPaused) return;
        SetPause(true);
    }

    // 继续按钮点击
    private void OnResumeButtonClick()
    {
        if (!_isPaused) return;
        SetPause(false);
    }

    // 设置暂停状态
    public void SetPause(bool pause)
    {
        _isPaused = pause;

        // 时间控制
        Time.timeScale = pause ? 0 : 1;

        // 音频控制
        if (muteOnPause)
        {
            if (pause)
            {
                if (mainMixer != null)
                    mainMixer.GetFloat("MasterVolume", out _prePauseVolume);
                MuteAudio(true);
            }
            else
            {
                MuteAudio(false);
            }
        }

        // UI更新
        if (pausePanel != null)
            pausePanel.SetActive(pause);

        Debug.Log($"游戏已{(pause ? "暂停" : "恢复")}" +
                 $" | 时间缩放: {Time.timeScale}" +
                 $" | 音频状态: {(AudioListener.pause ? "静音" : "正常")}");
    }

    // 音频静音控制
    private void MuteAudio(bool mute)
    {
        if (mainMixer != null)
        {
            mainMixer.SetFloat("MasterVolume", mute ? -80f : _prePauseVolume);
        }
        AudioListener.pause = mute;
    }

    // 强制恢复游戏状态
    private void ForceUnpause()
    {
        _isPaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        if (mainMixer != null)
            mainMixer.SetFloat("MasterVolume", 0f);
    }

    void OnDestroy()
    {
        ForceUnpause();
    }
}