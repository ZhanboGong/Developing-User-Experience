using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class RestartGame : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioMixer mainMixer; // 拖入你的主音频混合器（如果使用）
    public bool restoreAudioOnRestart = true;

    public void LoadScene(int sceneIndex)
    {
        RestoreAudio(); // 恢复音频
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadCurrentScene()
    {
        RestoreAudio(); // 恢复音频
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void RestoreAudio()
    {
        if (!restoreAudioOnRestart) return;

        // 方案A：使用AudioMixer
        if (mainMixer != null)
        {
            mainMixer.SetFloat("MasterVolume", 0f); // 恢复默认音量（0dB）
        }
        // 方案B：使用AudioListener
        else
        {
            AudioListener.volume = 1f;
        }
    }
}