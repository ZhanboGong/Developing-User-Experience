using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float timer = 60f;
    [SerializeField] private int targetPickups = 4; // 需要收集4个pickup

    [Header("UI References")]
    public TMP_Text timerText;
    public TMP_Text pickupCountText; // 修改变量名更贴切
    public GameObject winPanel;
    public GameObject[] stars;

    private int currentPickups = 0;
    private bool isTimeOut = false;
    private bool canClick = false;

    void Start()
    {
        // 初始化时统计场景中所有"pickup"（注意小写）
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("pickup");
        currentPickups = 0;
        pickupCountText.text = "(" + currentPickups + "/" + targetPickups +")"; // 显示为 0/4
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

    // Pickup收集方法（确保被pickup物体调用）
    public void OnPickupCollected()
    {
        if (isTimeOut) return;

        currentPickups++;
        pickupCountText.text = "(" + currentPickups + "/" + targetPickups + ")"; // 更新为 1/4

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

        // 根据收集比例显示星星
        float progress = (float)currentPickups / targetPickups;

        stars[0].SetActive(true); // 至少1星
        yield return new WaitForSeconds(0.5f);

        if (progress >= 0.5f) // 2个以上
        {
            stars[1].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        if (progress >= 1f) // 4个全部
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