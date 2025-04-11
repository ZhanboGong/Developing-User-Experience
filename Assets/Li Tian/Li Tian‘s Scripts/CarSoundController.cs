using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    // 音频剪辑
    public AudioClip engineStart;      // 引擎启动
    public AudioClip engineIdle;       // 怠速
    public AudioClip engineAccelerate; // 加速
    public AudioClip brake;            // 刹车

    // 音频源
    private AudioSource audioSource;

    // 怠速音效的持续播放控制
    private bool isIdlePlaying = false;

    void Start()
    {
        // 初始化音频源
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            Debug.LogError("AudioSource 组件未找到！");
            return;
        }

        // 游戏启动时播放引擎启动音效
        PlayOneShot(engineStart);

        // 开始怠速音效
        PlayIdle();
    }

    void Update()
    {
        // 检测玩家输入
        if (Input.GetKey(KeyCode.W))
        {
            // 按下 W 键时播放加速音效
            PlayAccelerate();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            // 按下空格键时播放刹车音效
            PlayBrake();
        }
        else
        {
            // 无操作时保持怠速
            if (!isIdlePlaying)
            {
                PlayIdle();
            }
        }
    }

    // 播放单次音效（如启动、刹车）
    void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // 播放怠速音效（循环）
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

    // 播放加速音效（与怠速同时播放）
    void PlayAccelerate()
    {
        // 暂停怠速音效（可选，根据需求调整）
        // audioSource.Pause();

        // 播放加速音效
        audioSource.PlayOneShot(engineAccelerate);

        // 加速结束后恢复怠速（示例：假设加速持续0.5秒）
        Invoke("ResumeIdle", 0.5f);
    }

    // 恢复怠速音效
    void ResumeIdle()
    {
        if (isIdlePlaying)
        {
            audioSource.UnPause();
        }
    }

    // 播放刹车音效
    void PlayBrake()
    {
        audioSource.PlayOneShot(brake);
    }
}