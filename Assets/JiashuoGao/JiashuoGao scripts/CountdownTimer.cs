using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    [Header("倒计时设置")]
    [SerializeField] private TextMeshProUGUI timerText; // 显示倒计时的UI文本
    [SerializeField] private float totalTime = 60f;     // 总倒计时时间(秒)
    [SerializeField] private TextMeshProUGUI gameOverText; // 新增：游戏结束文本
    
    private float currentTime;                          // 当前剩余时间
    private Coroutine timerCoroutine;                  // 计时器协程引用

    void Start()
    {
        // 初始化时重置计时器
        ResetTimer();
        
        // 确保游戏结束文本初始时不可见
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 开始倒计时
    /// </summary>
    public void StartTimer()
    {
        // 隐藏游戏结束文本（如果可见）
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        
        // 如果已经有计时器在运行，先停止
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        
        timerCoroutine = StartCoroutine(RunTimer());
    }

    /// <summary>
    /// 停止倒计时
    /// </summary>
    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    /// <summary>
    /// 重置倒计时
    /// </summary>
    public void ResetTimer()
    {
        currentTime = totalTime;
        UpdateTimerDisplay();
    }

    /// <summary>
    /// 倒计时协程
    /// </summary>
    private IEnumerator RunTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateTimerDisplay();
        }
        
        // 倒计时结束
        OnTimerEnd();
    }

    /// <summary>
    /// 更新倒计时显示
    /// </summary>
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            // 格式化为 00:00 的分钟:秒格式
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    /// <summary>
    /// 倒计时结束时的处理
    /// </summary>
    private void OnTimerEnd()
    {
        Debug.Log("倒计时结束");
        
        // 显示游戏结束文本
        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER";
            gameOverText.gameObject.SetActive(true);
        }
        
        // 这里可以添加其他游戏结束逻辑
        // 例如：停止玩家控制、显示重新开始按钮等
    }

    /// <summary>
    /// 获取当前剩余时间(秒)
    /// </summary>
    public float GetCurrentTime()
    {
        return currentTime;
    }

    /// <summary>
    /// 检查倒计时是否结束
    /// </summary>
    public bool IsTimeUp()
    {
        return currentTime <= 0f;
    }
}