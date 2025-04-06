using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform carTransform;  // 汽车的Transform
    [Range(1, 10)]
    public float followSpeed = 20f;  // 摄像机跟随速度，稍微提高以使跟随更灵敏
    [Range(1, 10)]
    public float lookSpeed = 20f;    // 摄像机旋转速度，增大旋转速度使得摄像机更迅速地朝向车

    public Vector3 offset = new Vector3(0, 2, -5);  // 更贴近的偏移量，调整为汽车后方且较近

    void FixedUpdate()
    {
        // 使摄像机始终朝向汽车
        Vector3 _lookDirection = carTransform.position - transform.position;
        Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);

        // 使摄像机固定在汽车后方，贴近汽车
        Vector3 _targetPos = carTransform.position + carTransform.TransformDirection(offset);  // 偏移量基于汽车的旋转方向
        transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
    }
}
