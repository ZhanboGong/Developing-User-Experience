using UnityEngine;
using TMPro; // ��Ӵ���

public class CarPickupHandler : MonoBehaviour
{
    public TextMeshProUGUI countText; // ʹ�� TextMeshProUGUI ����
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
        countText.text = "Score: " + count.ToString(); // ���� TextMeshPro �ı�
    }
}