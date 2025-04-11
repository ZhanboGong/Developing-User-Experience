using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // ������ƵԴ
    public AudioSource audioSource;

    void Start()
    {
        // ��ʼ��������
        audioSource.Play();
    }

    // ��ѡ����ͣ/�ָ�����
    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        audioSource.UnPause();
    }
}