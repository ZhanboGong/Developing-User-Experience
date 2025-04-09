using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // 公开访问的静态实例
    public static ScoreManager Instance { get; private set; }
    
    [Header("UI显示")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI winText; // 新增：胜利文本
    
    // 游戏设置
    [SerializeField] private int winScore = 10; // 胜利所需分数
    
    // 当前分数
    private int currentScore = 0;

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
        // 初始化时隐藏胜利文本
        if (winText != null)
        {
            winText.gameObject.SetActive(false);
        }
        UpdateScoreDisplay();
    }

    /// <summary>
    /// 增加分数
    /// </summary>
    public void AddPoints(int points)
    {
        currentScore += points;
        UpdateScoreDisplay();
        
        // 检查是否获胜
        if (currentScore >= winScore)
        {
            ShowWinMessage();
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
        UpdateScoreDisplay();
        
        // 重置时隐藏胜利文本
        if (winText != null)
        {
            winText.gameObject.SetActive(false);
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
    /// 显示胜利信息
    /// </summary>
    private void ShowWinMessage()
    {
        if (winText != null)
        {
            winText.text = "YOU WIN!";
            winText.gameObject.SetActive(true);
            
            // 可以在这里添加胜利音效或其他效果
        }
    }

    /// <summary>
    /// 检查是否已经获胜
    /// </summary>
    public bool HasWon()
    {
        return currentScore >= winScore;
    }
}