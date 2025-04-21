using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    [Header("旋转设置")]
    public float rotationSpeed = 180f; // 度/秒
    public Vector3 rotationAxis = Vector3.up; // 绕Y轴旋转
    
    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}