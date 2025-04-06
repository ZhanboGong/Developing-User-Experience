using UnityEngine;
using UnityEngine.UI;  // ���� UI �࣬�������з�����ʾ

public class CarPickupHandler : MonoBehaviour
{
    public AudioSource clickAudio;  // ���ڲ�����Ч
    public Text countText;  // ������ʾ����
    private int count = 0;  // ��ǰ����

    public void OnTriggerEnter(Collider other)
    {
        // ������������ "pickup" ��ǩ������ʱ
        if (other.CompareTag("pickup"))
        {
            Destroy(other.gameObject); // �����ռ���
            count += 1; // ����1��
            SetCountText(); // ���·�����ʾ
            clickAudio.Play(); // ���ŵ����Ч
        }

        // ������������ "Sphere" ��ǩ������ʱ
        if (other.CompareTag("Sphere"))
        {
            Destroy(other.gameObject); // �����ռ���
            count += 2; // ����2��
            SetCountText(); // ���·�����ʾ
            clickAudio.Play(); // ���ŵ����Ч
        }
    }

    // ���·�����ʾ
    public void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }
}
