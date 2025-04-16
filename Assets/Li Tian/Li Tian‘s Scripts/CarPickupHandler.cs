using UnityEngine;
using TMPro; // 添加此行

public class CarPickupHandler : MonoBehaviour
{
    public TextMeshProUGUI countText; // 使用 TextMeshProUGUI 类型
    public AudioSource clickAudio;
    private int count = 0;

    // 引入 UIManager 脚本实例
    public UIManager uiManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickup"))
        {
            Destroy(other.gameObject);
            count += 1;
            SetCountText();
            clickAudio.Play();
        }
        else if (other.CompareTag("Trophy"))
        {
            Destroy(other.gameObject);
            // 通知 UIManager 触发胜利条件
            if (uiManager != null)
            {
                uiManager.TriggerWin();
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString(); // 更新 TextMeshPro 文本
    }
}