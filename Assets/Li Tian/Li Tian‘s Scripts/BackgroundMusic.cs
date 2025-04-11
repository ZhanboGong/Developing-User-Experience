using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // 引用音频源
    public AudioSource audioSource;

    void Start()
    {
        // 开始播放音乐
        audioSource.Play();
    }

    // 可选：暂停/恢复音乐
    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        audioSource.UnPause();
    }
}