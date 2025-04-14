using UnityEngine;

public class SimonArrow : MonoBehaviour
{
    public Transform target;         // 绑定点位目标（如Helipad）
    public float arrowSpeed = 5f;   // 旋转速度（可选平滑过渡）
    public float hideDistance = 5f;
    void Update()
    {
        if (target == null) return;

        // 计算水平距离（忽略Y轴）
        float distance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(target.position.x, 0, target.position.z)
        );

        // 根据距离显示/隐藏箭头
        gameObject.SetActive(distance > hideDistance);

        // 忽略垂直高度，仅计算XZ平面方向
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 direction = targetPos - transform.position;

        // 直接指向目标（无平滑过渡）
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // 可选：平滑旋转（启用时取消注释）
        // transform.rotation = Quaternion.Slerp(
        //     transform.rotation,
        //     Quaternion.LookRotation(direction),
        //     arrowSpeed * Time.deltaTime
        // );
    }
}