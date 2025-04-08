using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public float volume = 0.5f; // Ĭ������Ϊ50%

    void Start()
    {
        // ȷ�������ڳ����л�ʱ���ᱻ����
        DontDestroyOnLoad(gameObject);
        // ���ټ��ر�����������ã�ֱ��ʹ��Ĭ��ֵ
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        // ����ȫ������
        AudioListener.volume = volume;
    }
}
