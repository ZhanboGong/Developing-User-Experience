using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 公开访问的静态实例
    public static ScoreManager Instance { get; private set; }
    
    [Header("UI显示")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject winPanel; // 改为胜利面板
    
    [Header("胜利设置")]
    [SerializeField] private int winScore = 10; // 胜利所需分数
    [SerializeField] private Button winRestartButton; // 胜利面板的重置按钮
    
    // 当前分数
    private int currentScore = 0;
    private bool hasWon = false;

    private void Awake()
    {
        // 单例模式初始化
        if (Instance == null)
        {
            Instance = this;
            InitializeUI();
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
        
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            // 可以在这里添加胜利音效或其他效果
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