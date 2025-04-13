using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement; // 新增场景管理命名空间

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
    public TextMeshProUGUI successScoreText; // 新增分数显示文本
    private CountdownTimer timer;

    // 新增重启游戏方法
    public void RestartGame()
    {
        Time.timeScale = 1f; // 恢复时间流速
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.mass = 1f;
        count = 0;
        SetCountText();
        timer = FindObjectOfType<CountdownTimer>();
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Helipad"))
        {
            int displayScore = Mathf.Min(count, 15); // 限制最大显示15分
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
}