using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class SimonCarCtrl : MonoBehaviour
{
    private Rigidbody rb;
    private float moveX, moveY;
    private float moveSpeed = 15f;
    private float turnSpeed = 1f;

    private int count;
    public TextMeshProUGUI countText;
    public AudioSource PickupAudio, BoomAudio;
    public GameObject successPanel;
    public TextMeshProUGUI successScoreText;

    [Header("Pause")]
    public GameObject pausePanel; // 确保在Unity编辑器中绑定此面板
    public TextMeshProUGUI pauseTimeText;
    public TextMeshProUGUI pauseScoreText;

    private CountdownTimer timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.mass = 1f;
        count = 0;
        SetCountText();
        timer = FindObjectOfType<CountdownTimer>();
        pausePanel.SetActive(false); // 初始隐藏暂停面板
    }

    public void OnMove(InputValue moveValue)
    {
        Vector2 moveVector = moveValue.Get<Vector2>();
        moveX = moveVector.x;
        moveY = moveVector.y;
    }

    void FixedUpdate()
    {
        float turnDirection = moveX * Mathf.Sign(moveY);
        rb.angularVelocity = Vector3.up * turnDirection * turnSpeed;

        Vector3 desiredForce = transform.forward * moveY * moveSpeed;
        rb.AddForce(desiredForce, ForceMode.Force);

        if (rb.velocity.magnitude > 18f)
            rb.velocity = rb.velocity.normalized * 18f;

        if (Mathf.Abs(transform.eulerAngles.x) > 0.1f || 
            Mathf.Abs(transform.eulerAngles.z) > 0.1f)
        {
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }
    }

    // 点击暂停按钮调用此方法
    public void TogglePause()
    {
        bool isPaused = !pausePanel.activeSelf;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        if (isPaused)
        {
            // 更新暂停界面数据
            pauseTimeText.text = timer.GetRemainingTimeFormatted();
            pauseScoreText.text = "Score: " + count.ToString();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Helipad"))
        {
            int displayScore = Mathf.Min(count, 15);
            successScoreText.text = $"You got {displayScore}/15 scores";
            successPanel.SetActive(true);
            timer.StopCountdown();
            Time.timeScale = 0f;
        }
        else if (other.CompareTag("pickup+1")) CollectItem(other, 1);
        else if (other.CompareTag("pickup+2")) CollectItem(other, 2);
        else if (other.CompareTag("pickup-1")) CollectItem(other, -5);
    }

    private void CollectItem(Collider item, int points)
    {
        item.gameObject.SetActive(false);
        count += points;
        SetCountText();

        if (item.CompareTag("pickup+1") || item.CompareTag("pickup+2"))
            PickupAudio.Play();
        else if (item.CompareTag("pickup-1"))
            BoomAudio.Play();
    }

    public void SetCountText() => countText.text = "Score: " + count.ToString();

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}