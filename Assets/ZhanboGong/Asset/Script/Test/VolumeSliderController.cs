using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    public Slider volumeSlider;
    public VolumeController volumeController;

    void Start()
    {
        // 设置滑动条的初始值为默认值
        volumeSlider.value = 0.5f;
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);
    }

    void OnVolumeSliderValueChanged(float newValue)
    {
        // 更新音量控制器的音量值
        volumeController.volume = newValue;
        volumeController.UpdateVolume();
    }
}