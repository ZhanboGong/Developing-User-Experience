using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float timer = 60f;
    [SerializeField] private int targetPickups = 3; // 保留目标数量

    [Header("UI References")]
    public TMP_Text timerText;
    public TMP_Text pickupCountText; // 保留并继续更新
    public GameObject winPanel;
    public TMP_Text scoreText;

    [Header("Audio Settings")]
    public AudioClip victorySound;
    public AudioSource audioSource;

    private int currentPickups = 0;
    private bool isTimeOut = false;
    private bool canClick = false;
    private bool hasPlayedVictorySound = false;

    void Start()
    {
        UpdatePickupUI(); // 初始化物品数量显示

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (!isTimeOut)
        {
            Timer();
        }
    }

    // 碰撞奖杯触发
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trophy"))
        {
            TriggerWin();
        }
    }

    // 触发器奖杯触发
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trophy"))
        {
            TriggerWin();
        }
    }

    private void Timer()
    {
        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Max(0, timer).ToString("F2");

        if (timer <= 0)
        {
            timer = 0;
            TriggerWin(); // 时间结束触发
        }
    }

    // 物品收集功能
    public void OnPickupCollected()
    {
        currentPickups = Mathf.Min(currentPickups + 1, targetPickups);
        UpdatePickupUI();

    }

    // UI 更新方法
    private void UpdatePickupUI()
    {
        pickupCountText.text = $"({currentPickups}/{targetPickups})";
    }

    // 合并的胜利触发方法
    public void TriggerWin() // 修改为 public
    {
        if (isTimeOut) return;

        isTimeOut = true;
        ShowWinPanel();
    }

    private void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);

            if (scoreText != null)
            {
                scoreText.text = $"Final Time: {60 - timer:F1}s\nItems Collected: {currentPickups}/{targetPickups}";
            }

            if (!hasPlayedVictorySound && victorySound != null && audioSource != null)
            {
                audioSource.PlayOneShot(victorySound);
                hasPlayedVictorySound = true;
            }
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