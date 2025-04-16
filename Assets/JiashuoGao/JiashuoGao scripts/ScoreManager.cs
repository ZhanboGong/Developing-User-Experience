using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 公开访问的静态实例
    public static ScoreManager Instance { get; private set; }
    
    [Header("UI显示")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject winPanel; // 胜利面板
    [SerializeField] private TextMeshProUGUI finalScoreText; // 胜利面板上的最终得分文本
    
    [Header("胜利设置")]
    [SerializeField] private int winScore = 10; // 胜利所需分数
    [SerializeField] private Button winRestartButton; // 胜利面板的重置按钮
    [SerializeField] private AudioClip winSound; // 胜利音效
    [SerializeField] private float winSoundVolume = 1.0f; // 音效音量
    
    // 当前分数
    private int currentScore = 0;
    private bool hasWon = false;
    private AudioSource audioSource; // 音频源组件

    private void Awake()
    {
        // 单例模式初始化
        if (Instance == null)
        {
            Instance = this;
            InitializeUI();
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeUI()
    {
        // 初始化时隐藏胜利面板
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        
        // 设置胜利面板的重置按钮
        if (winRestartButton != null)
        {
            winRestartButton.onClick.AddListener(ResetGame);
        }
        
        UpdateScoreDisplay();
    }

    private void InitializeAudio()
    {
        // 添加音频源组件
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = winSoundVolume;
    }

    /// <summary>
    /// 增加分数
    /// </summary>
    public void AddPoints(int points)
    {
        if (hasWon) return; // 如果已经赢了，不再增加分数
        
        currentScore += points;
        UpdateScoreDisplay();
        
        // 检查是否获胜
        if (currentScore >= winScore)
        {
            WinGame();
        }
    }

    /// <summary>
    /// 获取当前分数
    /// </summary>
    public int GetCurrentScore()
    {
        return currentScore;
    }

    /// <summary>
    /// 重置分数
    /// </summary>
    public void ResetScore()
    {
        currentScore = 0;
        hasWon = false;
        UpdateScoreDisplay();
        
        // 重置时隐藏胜利面板
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    /// <summary>
    /// 更新分数显示
    /// </summary>
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    /// <summary>
    /// 游戏胜利逻辑
    /// </summary>
    private void WinGame()
    {
        hasWon = true;
        
        // 播放胜利音效
        if (winSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(winSound);
        }
        
        if (winPanel != null)
        {
            // 显示最终得分
            if (finalScoreText != null)
            {
                finalScoreText.text = "Final Score: " + currentScore.ToString();
            }
            
            winPanel.SetActive(true);
        }
        
        // 通知倒计时系统游戏已胜利
        if (CountdownTimer.Instance != null)
        {
            CountdownTimer.Instance.OnGameWon();
        }
    }

    /// <summary>
    /// 重置游戏
    /// </summary>
    private void ResetGame()
    {
        ResetScore();
        
        // 通知倒计时系统重新开始
        if (CountdownTimer.Instance != null)
        {
            CountdownTimer.Instance.ResetTimer();
            CountdownTimer.Instance.StartTimer();
        }
    }

    /// <summary>
    /// 检查是否已经获胜
    /// </summary>
    public bool HasWon()
    {
        return hasWon;
    }
}