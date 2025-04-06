using UnityEngine;
using UnityEngine.UI;  // 引用 UI 类，假如你有分数显示

public class CarPickupHandler : MonoBehaviour
{
    public AudioSource clickAudio;  // 用于播放音效
    public Text countText;  // 用于显示分数
    private int count = 0;  // 当前分数

    public void OnTriggerEnter(Collider other)
    {
        // 当车子碰到有 "pickup" 标签的物体时
        if (other.CompareTag("pickup"))
        {
            Destroy(other.gameObject); // 销毁收集物
            count += 1; // 增加1分
            SetCountText(); // 更新分数显示
            clickAudio.Play(); // 播放点击音效
        }

        // 当车子碰到有 "Sphere" 标签的物体时
        if (other.CompareTag("Sphere"))
        {
            Destroy(other.gameObject); // 销毁收集物
            count += 2; // 增加2分
            SetCountText(); // 更新分数显示
            clickAudio.Play(); // 播放点击音效
        }
    }

    // 更新分数显示
    public void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }
}
