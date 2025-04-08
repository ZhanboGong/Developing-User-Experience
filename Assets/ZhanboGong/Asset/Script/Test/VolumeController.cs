using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public float volume = 0.5f; // 默认音量为50%

    void Start()
    {
        // 确保对象在场景切换时不会被销毁
        DontDestroyOnLoad(gameObject);
        // 不再加载保存的音量设置，直接使用默认值
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        // 设置全局音量
        AudioListener.volume = volume;
    }
}
