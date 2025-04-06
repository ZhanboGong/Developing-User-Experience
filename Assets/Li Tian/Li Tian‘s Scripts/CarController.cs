using UnityEngine;

public class CarController : MonoBehaviour
{
    // 基础参数
    public float moveSpeed = 30f;     // 前进/后退速度
    public float turnSpeed = 30f;     // 转向灵敏度
    public float maxSpeed = 20f;      // 最大速度
    private Rigidbody rb;

    // Wheel Collider 控制
    public WheelCollider[] wheels;    // 顺序: 左前, 右前, 左后, 右后

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;     // 防止侧翻
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // 方法1: 直接控制车轮物理 (推荐)
        if (wheels != null && wheels.Length == 4)
        {
            // 驱动后轮
            wheels[2].motorTorque = moveInput * moveSpeed * 100;  // 左后轮
            wheels[3].motorTorque = moveInput * moveSpeed * 100;  // 右后轮

            // 转向前轮
            wheels[0].steerAngle = turnInput * turnSpeed;         // 左前轮
            wheels[1].steerAngle = turnInput * turnSpeed;         // 右前轮
        }
        // 方法2: 简单物理控制 (备用)
        else
        {
            rb.AddForce(transform.forward * moveInput * moveSpeed);
            transform.Rotate(0f, turnInput * turnSpeed * Time.deltaTime, 0f);
        }

        // 限速
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

    }
}