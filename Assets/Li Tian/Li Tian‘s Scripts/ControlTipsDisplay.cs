using UnityEngine;
using TMPro;

public class ControlTipsDisplay : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text tipsText; // 仅拖入操作说明的TextMeshPro文本组件
    // 移除了对整个panel的控制

    [TextArea]
    public string controlTips = "W: Acceleration \n S: Reverse \n A: Left Turn \n D: Right Turn \n Space: Brake \n Release W+Space+Direction Key A or D: Drift";

    [Header("Settings")]
    public float displayTime = 8f; // 显示时长

    void Start()
    {
        // 初始化文本显示
        if (tipsText != null)
        {
            tipsText.text = controlTips;
            tipsText.gameObject.SetActive(true); // 确保文本是激活状态

            // 8秒后仅隐藏文本
            Invoke("HideTipsText", displayTime);
        }
    }

    void HideTipsText()
    {
        if (tipsText != null)
        {
            tipsText.gameObject.SetActive(false); // 仅隐藏文本，不影响其他UI
        }
    }
}