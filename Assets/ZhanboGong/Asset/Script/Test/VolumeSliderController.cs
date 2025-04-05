using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    public Slider volumeSlider;
    public VolumeController volumeController;

    void Start()
    {
        // ���û������ĳ�ʼֵΪĬ��ֵ
        volumeSlider.value = 0.5f;
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);
    }

    void OnVolumeSliderValueChanged(float newValue)
    {
        // ��������������������ֵ
        volumeController.volume = newValue;
        volumeController.UpdateVolume();
    }
}