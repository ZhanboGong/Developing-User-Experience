using UnityEngine;
using TMPro; // 添加此行

public class CarPickupHandler : MonoBehaviour
{
    public TextMeshProUGUI countText; // 使用 TextMeshProUGUI 类型
    public AudioSource clickAudio;
    private int count = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickup"))
        {
            Destroy(other.gameObject);
            count += 1;
            SetCountText();
            clickAudio.Play();
        }
        else if (other.CompareTag("Sphere"))
        {
            Destroy(other.gameObject);
            count += 2;
            SetCountText();
            clickAudio.Play();
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString(); // 更新 TextMeshPro 文本
    }
}