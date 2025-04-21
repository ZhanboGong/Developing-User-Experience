using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("跟随目标")]
    public Transform target;  // 拖入小车对象
    
    [Header("相机位置")]
    public Vector3 offset = new Vector3(0, 8, -12); // 相机相对于小车的位置
    
    [Header("跟随平滑度")]
    [Range(0.1f, 1f)]
    public float smoothSpeed = 0.5f;
    
    void LateUpdate()
    {
        if (target == null) return;
        
        // 计算目标位置（考虑小车的旋转）
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        
        // 平滑移动
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position, 
            desiredPosition, 
            smoothSpeed);
            
        transform.position = smoothedPosition;
        
        // 始终看向小车
        transform.LookAt(target);
    }
}
