using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 返回主菜单
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        BGMManager.Instance.StopBGM(); // 停止BGM
        SceneManager.LoadScene(0);
    }

    // 重新开始游戏
    public void RestartGame()
    {
        Time.timeScale = 1f;
        BGMManager.Instance.PlayBGM(); // 重新播放BGM
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}