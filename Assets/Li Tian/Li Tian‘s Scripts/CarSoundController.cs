using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    // ��Ƶ����
    public AudioClip engineStart;      // ��������
    public AudioClip engineIdle;       // ����
    public AudioClip engineAccelerate; // ����
    public AudioClip brake;            // ɲ��

    // ��ƵԴ
    private AudioSource audioSource;

    // ������Ч�ĳ������ſ���
    private bool isIdlePlaying = false;

    void Start()
    {
        // ��ʼ����ƵԴ
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            Debug.LogError("AudioSource ���δ�ҵ���");
            return;
        }

        // ��Ϸ����ʱ��������������Ч
        PlayOneShot(engineStart);

        // ��ʼ������Ч
        PlayIdle();
    }

    void Update()
    {
        // ����������
        if (Input.GetKey(KeyCode.W))
        {
            // ���� W ��ʱ���ż�����Ч
            PlayAccelerate();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            // ���¿ո��ʱ����ɲ����Ч
            PlayBrake();
        }
        else
        {
            // �޲���ʱ���ֵ���
            if (!isIdlePlaying)
            {
                PlayIdle();
            }
        }
    }

    // ���ŵ�����Ч����������ɲ����
    void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // ���ŵ�����Ч��ѭ����
    void PlayIdle()
    {
        if (!isIdlePlaying)
        {
            audioSource.clip = engineIdle;
            audioSource.loop = true;
            audioSource.Play();
            isIdlePlaying = true;
        }
    }

    // ���ż�����Ч���뵡��ͬʱ���ţ�
    void PlayAccelerate()
    {
        // ��ͣ������Ч����ѡ���������������
        // audioSource.Pause();

        // ���ż�����Ч
        audioSource.PlayOneShot(engineAccelerate);

        // ���ٽ�����ָ����٣�ʾ����������ٳ���0.5�룩
        Invoke("ResumeIdle", 0.5f);
    }

    // �ָ�������Ч
    void ResumeIdle()
    {
        if (isIdlePlaying)
        {
            audioSource.UnPause();
        }
    }

    // ����ɲ����Ч
    void PlayBrake()
    {
        audioSource.PlayOneShot(brake);
    }
}