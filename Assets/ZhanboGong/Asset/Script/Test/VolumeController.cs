using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public float volume = 0.5f; // 默认音量为50%

    void Start()
    {
        // 不再加载保存的音量设置，直接使用默认值
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        // 设置全局音量
        AudioListener.volume = volume;
    }

    public void SaveVolumeSetting()
    {
        // 不再保存音量设置
    }
}
