using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    public Slider volumeSlider;
    private VolumeController volumeController;

    void Start()
    {
        // 获取 VolumeController 的实例
        volumeController = FindObjectOfType<VolumeController>();
        if (volumeController == null)
        {
            Debug.LogError("VolumeController not found!");
            return;
        }

        // 设置滑动条的初始值为当前音量
        volumeSlider.value = volumeController.volume;
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);
    }

    void OnVolumeSliderValueChanged(float newValue)
    {
        // 更新音量控制器的音量值
        volumeController.volume = newValue;
        volumeController.UpdateVolume();
    }
}