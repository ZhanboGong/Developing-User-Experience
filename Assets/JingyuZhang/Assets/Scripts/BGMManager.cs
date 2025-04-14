using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;  // 单例

    [Header("BGM配置")]
    public AudioClip gameBGM;          // 游戏背景音乐
    public AudioSource audioSource;    // 绑定的音频源组件

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeAudioSource();
            
            // 加载保存的音量
            audioSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 新增：设置音量方法
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume); // 保存到本地
    }

    private void Start()
    {
        PlayBGM(); // 游戏启动时播放
        SceneManager.sceneLoaded += OnSceneLoaded; // 监听场景加载事件
    }

    // 场景加载时触发（重新开始游戏时调用）
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 检查 Instance 是否为 null，以防止访问已被销毁的 BGMManager 实例
        if (Instance != null && Instance.audioSource == null)
        {
            Instance.InitializeAudioSource();
        }
        else if (Instance != null && !Instance.audioSource.isPlaying)
        {
            Instance.PlayBGM(); // 重新播放
        }
    }

    // 播放BGM
    public void PlayBGM()
    {
        if (audioSource != null && gameBGM != null)
        {
            audioSource.clip = gameBGM;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // 暂停BGM（用于暂停页面）
    public void PauseBGM()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    // 恢复BGM（从暂停中恢复）
    public void ResumeBGM()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }

    // 停止BGM（用于结束页面）
    public void StopBGM()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    // 初始化 AudioSource
    void InitializeAudioSource()
    {
        if (gameObject.GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        if (gameBGM != null && audioSource != null)
        {
            audioSource.clip = gameBGM;
            audioSource.loop = true;
        }
    }
}