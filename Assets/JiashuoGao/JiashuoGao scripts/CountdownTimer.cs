using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    // 公开访问的静态实例
    public static CountdownTimer Instance { get; private set; }
    
    [Header("倒计时设置")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float totalTime = 60f;
    
    [Header("游戏结束界面")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverTitle;
    [SerializeField] private TextMeshProUGUI encouragementText;
    [SerializeField] private Button restartButton;
    
    [Header("需要隐藏的UI元素")]
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject timeText;
    
    [Header("金币设置")]
    [SerializeField] private string coinTag = "Goldcoins"; // 金币对象的标签
    
    [Header("鼓励性文案")]
    [SerializeField] private string[] encouragementMessages = {
       "Keep up the good work, next time will be better!" ,
        "I almost succeeded. Let's try again!" ,
        "Every attempt is progress!" ,
        "Don't give up, keep challenging!"
    };
    
    private float currentTime;
    private Coroutine timerCoroutine;
    private bool gameWon = false;

    private void Awake()
    {
        // 单例模式初始化
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResetTimer();
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    public void StartTimer()
    {
        gameWon = false;
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        if (scoreText != null) scoreText.SetActive(true);
        if (timeText != null) timeText.SetActive(true);
        
        // 重新激活所有金币（如果重新开始游戏）
        ReactivateAllCoins();
        
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        
        timerCoroutine = StartCoroutine(RunTimer());
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    public void ResetTimer()
    {
        currentTime = totalTime;
        UpdateTimerDisplay();
    }

    private IEnumerator RunTimer()
    {
        while (currentTime > 0 && !gameWon)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateTimerDisplay();
        }
        
        // 只有游戏未胜利时才显示游戏结束
        if (!gameWon)
        {
            OnTimerEnd();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void OnTimerEnd()
    {
        Debug.Log("倒计时结束");
        
        // 隐藏分数和时间显示
        if (scoreText != null) scoreText.SetActive(false);
        if (timeText != null) timeText.SetActive(false);
        
        // 禁用所有金币对象
        DeactivateAllCoins();
        
        // 显示游戏结束面板
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            
            if (encouragementMessages.Length > 0 && encouragementText != null)
            {
                int randomIndex = Random.Range(0, encouragementMessages.Length);
                encouragementText.text = encouragementMessages[randomIndex];
            }
        }
    }

    /// <summary>
    /// 禁用所有金币对象（使用SetActive(false)）
    /// </summary>
    private void DeactivateAllCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag(coinTag);
        foreach (GameObject coin in coins)
        {
            coin.SetActive(false);
        }
        Debug.Log($"已禁用 {coins.Length} 个金币对象");
    }

    /// <summary>
    /// 重新激活所有金币对象（用于重新开始游戏）
    /// </summary>
    private void ReactivateAllCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag(coinTag);
        foreach (GameObject coin in coins)
        {
            coin.SetActive(true);
        }
        Debug.Log($"已重新激活 {coins.Length} 个金币对象");
    }

    /// <summary>
    /// 当游戏胜利时调用
    /// </summary>
    public void OnGameWon()
    {
        gameWon = true;
        StopTimer();
        
        // 隐藏时间和分数显示
        if (scoreText != null) scoreText.SetActive(false);
        if (timeText != null) timeText.SetActive(false);
    }

    private void RestartGame()
    {
        ResetTimer();
        StartTimer();
        
        // 重置分数
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public bool IsTimeUp()
    {
        return currentTime <= 0f;
    }
}