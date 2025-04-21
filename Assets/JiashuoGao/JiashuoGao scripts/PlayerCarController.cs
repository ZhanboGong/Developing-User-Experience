using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCarController : MonoBehaviour
{
    [Header("移动参数")]
    public float engineForce = 120000f;
    public float maxSpeed = 85f;
    public float steeringTorque = 28000f;
    
    [Header("按键设置")]
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode brakeKey = KeyCode.Space;

    private Rigidbody rb;
    private float currentSpeed;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // 降低重心防翻车
    }

    void FixedUpdate()
    {
        currentSpeed = rb.velocity.magnitude * 8.6f; // 转为km/h
        
        // 前进/后退
        if (Input.GetKey(forwardKey))
        {
            if(currentSpeed < maxSpeed)
                rb.AddForce(transform.forward * engineForce);
        }
        else if (Input.GetKey(backwardKey))
        {
            if(currentSpeed < maxSpeed * 0.5f) // 倒车限速
                rb.AddForce(-transform.forward * engineForce * 0.7f);
        }
        
        // 转向（只有移动时才有效）
        if (currentSpeed > 0.1f)
        {
            if (Input.GetKey(leftKey))
                rb.AddTorque(-transform.up * steeringTorque);
            if (Input.GetKey(rightKey))
                rb.AddTorque(transform.up * steeringTorque);
        }
        
        // 刹车
        if (Input.GetKey(brakeKey))
        {
            rb.AddForce(-rb.velocity.normalized * engineForce * 2f);
        }
    }
}