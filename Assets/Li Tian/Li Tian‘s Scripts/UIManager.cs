using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float timer = 60f;
    [SerializeField] private int targetPickups = 4; // ��Ҫ�ռ�4��pickup

    [Header("UI References")]
    public TMP_Text timerText;
    public TMP_Text pickupCountText; // �޸ı�����������
    public GameObject winPanel;
    public GameObject[] stars;

    private int currentPickups = 0;
    private bool isTimeOut = false;
    private bool canClick = false;

    void Start()
    {
        // ��ʼ��ʱͳ�Ƴ���������"pickup"��ע��Сд��
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("pickup");
        currentPickups = 0;
        pickupCountText.text = "(" + currentPickups + "/" + targetPickups +")"; // ��ʾΪ 0/4
    }

    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (!isTimeOut)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2");

            if (timer <= 0)
            {
                TimeOut();
            }
        }
    }

    // Pickup�ռ�������ȷ����pickup������ã�
    public void OnPickupCollected()
    {
        if (isTimeOut) return;

        currentPickups++;
        pickupCountText.text = "(" + currentPickups + "/" + targetPickups + ")"; // ����Ϊ 1/4

        if (currentPickups >= targetPickups)
        {
            WinGame();
        }
    }

    private void TimeOut()
    {
        isTimeOut = true;
        timerText.text = "00:00";
        StartCoroutine(ShowStars());
    }

    private void WinGame()
    {
        isTimeOut = true;
        StartCoroutine(ShowStars());
    }

    IEnumerator ShowStars()
    {
        winPanel.SetActive(true);

        // �����ռ�������ʾ����
        float progress = (float)currentPickups / targetPickups;

        stars[0].SetActive(true); // ����1��
        yield return new WaitForSeconds(0.5f);

        if (progress >= 0.5f) // 2������
        {
            stars[1].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        if (progress >= 1f) // 4��ȫ��
        {
            stars[2].SetActive(true);
        }

        canClick = true;
    }

    public void RestartButton()
    {
        if (canClick)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}