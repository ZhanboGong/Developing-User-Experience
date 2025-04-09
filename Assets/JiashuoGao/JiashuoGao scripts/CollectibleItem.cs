using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1; // 该物品的分数值

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 调用得分系统
            ScoreManager.Instance.AddPoints(scoreValue);
            
            // 销毁收集物
            Destroy(gameObject);
        }
    }
}
