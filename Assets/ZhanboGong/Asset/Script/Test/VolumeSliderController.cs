using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    public Slider volumeSlider;
    private VolumeController volumeController;

    void Start()
    {
        // ��ȡ VolumeController ��ʵ��
        volumeController = FindObjectOfType<VolumeController>();
        if (volumeController == null)
        {
            Debug.LogError("VolumeController not found!");
            return;
        }

        // ���û������ĳ�ʼֵΪ��ǰ����
        volumeSlider.value = volumeController.volume;
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);
    }

    void OnVolumeSliderValueChanged(float newValue)
    {
        // ��������������������ֵ
        volumeController.volume = newValue;
        volumeController.UpdateVolume();
    }
}