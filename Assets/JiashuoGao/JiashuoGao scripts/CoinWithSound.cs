using UnityEngine;

public class CoinWithSound : MonoBehaviour
{
    [Header("音效设置")]
    public AudioClip collectSound;  // 收集音效
    
    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            // 播放音效
            if(collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            
            Destroy(gameObject);
        }
    }
}
