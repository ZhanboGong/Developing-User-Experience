using UnityEngine;

public class CarController : MonoBehaviour
{
    // ��������
    public float moveSpeed = 30f;     // ǰ��/�����ٶ�
    public float turnSpeed = 30f;     // ת��������
    public float maxSpeed = 20f;      // ����ٶ�
    private Rigidbody rb;

    // Wheel Collider ����
    public WheelCollider[] wheels;    // ˳��: ��ǰ, ��ǰ, ���, �Һ�

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;     // ��ֹ�෭
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // ����1: ֱ�ӿ��Ƴ������� (�Ƽ�)
        if (wheels != null && wheels.Length == 4)
        {
            // ��������
            wheels[2].motorTorque = moveInput * moveSpeed * 100;  // �����
            wheels[3].motorTorque = moveInput * moveSpeed * 100;  // �Һ���

            // ת��ǰ��
            wheels[0].steerAngle = turnInput * turnSpeed;         // ��ǰ��
            wheels[1].steerAngle = turnInput * turnSpeed;         // ��ǰ��
        }
        // ����2: ��������� (����)
        else
        {
            rb.AddForce(transform.forward * moveInput * moveSpeed);
            transform.Rotate(0f, turnInput * turnSpeed * Time.deltaTime, 0f);
        }

        // ����
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

    }
}