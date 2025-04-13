using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // 新增场景管理命名空间


public class CountdownTimer : MonoBehaviour
{
    public float totalSeconds = 60f;
    public Color defaultColor = Color.white;
    public Color warningColor = Color.red;
    public GameObject failPanel;

    private Text displayText;
    private bool isCountingDown = true;
    private float remainingTime;
    private Coroutine countdownCoroutine;

    public delegate void TimerFinishedDelegate();
    public event TimerFinishedDelegate OnTimerFinished;

    // 新增重启游戏方法
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Start()
    {
        remainingTime = totalSeconds;
        displayText = GetComponent<Text>();
        if (displayText == null)
        {
            Debug.LogError("未找到 Text 组件！");
            return;
        }

        displayText.text = FormatTime(remainingTime);
        displayText.color = defaultColor;
        countdownCoroutine = StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        while (isCountingDown && remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            displayText.text = FormatTime(remainingTime);
            if (remainingTime <= 10f)
            {
                displayText.color = warningColor;
            }
            else
            {
                displayText.color = defaultColor;
            }
            yield return null;
        }

        if (failPanel != null)
        {
            failPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        if (OnTimerFinished != null)
            OnTimerFinished();

        displayText.text = "Time's Up!";
        displayText.color = warningColor;
    }

    private string FormatTime(float time)
    {
        time = Mathf.Max(time, 0f);
        int seconds = (int)(time % 60f);
        int milliseconds = (int)((time * 1000f) % 1000f);
        return string.Format("{0:D2}:{1:D3}", seconds, milliseconds);
    }

    public void StopCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
        isCountingDown = false;
    }

    public void Pause() => isCountingDown = false;
    public void Resume()
    {
        isCountingDown = true;
        if (countdownCoroutine == null)
            countdownCoroutine = StartCoroutine(CountdownRoutine());
    }

    public void ResetTimer()
    {
        remainingTime = totalSeconds;
        displayText.text = FormatTime(remainingTime);
        displayText.color = defaultColor;
        isCountingDown = true;
        countdownCoroutine = StartCoroutine(CountdownRoutine());
    }
}