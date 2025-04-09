using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CountdownTimer timer; // 拖拽赋值
    
    void Start()
    {
        // 游戏开始时自动启动倒计时
        StartCountdown();
    }
    
    public void StartCountdown()
    {
        timer.ResetTimer(); // 重置时间
        timer.StartTimer(); // 开始计时
    }
}