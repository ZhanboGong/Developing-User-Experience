using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float timer = 60f;
    [SerializeField] private int targetPickups = 4;

    [Header("UI References")]
    public TMP_Text timerText;
    public TMP_Text pickupCountText;
    public GameObject winPanel;
    public TMP_Text scoreText;

    [Header("Audio Settings")]  // 新增音频设置区域
    public AudioClip victorySound;  // 胜利音效
    public AudioSource audioSource;  // 音频源组件

    private int currentPickups = 0;
    private bool isTimeOut = false;
    private bool canClick = false;
    private bool hasPlayedVictorySound = false;  // 防止重复播放

    void Start()
    {
        UpdatePickupUI();

        // 初始时隐藏winPanel
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        // 确保有AudioSource组件
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

    private void Timer()
    {
        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Max(0, timer).ToString("F2");

        if (timer <= 0)
        {
            timer = 0;
            TimeOut();
        }
    }

    public void OnPickupCollected()
    {
        if (isTimeOut) return;

        currentPickups = Mathf.Min(currentPickups + 1, targetPickups);
        UpdatePickupUI();

        if (currentPickups >= targetPickups)
        {
            WinGame();
        }
    }

    private void UpdatePickupUI()
    {
        pickupCountText.text = $"({currentPickups}/{targetPickups})";
    }

    private void TimeOut()
    {
        isTimeOut = true;
        timerText.text = "00:00";
        ShowWinPanel();
    }

    private void WinGame()
    {
        isTimeOut = true;
        ShowWinPanel();
    }

    private void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);

            // 更新得分显示
            if (scoreText != null)
            {
                scoreText.text = $"Your Score: {currentPickups}";
            }

            // 播放胜利音效（仅播放一次）
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