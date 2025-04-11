using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LoadManager : MonoBehaviour
{
    // 引用UI组件
    public Slider progressBar;
    public TextMeshProUGUI loadingText;
    public Canvas loadingCanvas;

    // 加载场景的索引
    private int sceneIndex;

    // 加载进度
    private float loadingProgress = 0f;

    // 加载动画的持续时间（秒）
    public float loadingDuration = 3f;

    private void Start()
    {
        // 初始化加载Canvas为隐藏状态
        loadingCanvas.gameObject.SetActive(false);
    }

    // 开始加载场景
    public void LoadScene(int index)
    {
        sceneIndex = index;

        // 显示加载Canvas
        loadingCanvas.gameObject.SetActive(true);

        // 模拟加载进度
        StartCoroutine(SimulateLoading());
    }

    // 模拟加载进度
    private IEnumerator SimulateLoading()
    {
        // 重置加载进度
        loadingProgress = 0f;
        progressBar.value = 0f;
        loadingText.text = "Loading... 0%";

        // 模拟加载动画
        while (loadingProgress < 1f)
        {
            // 平滑更新进度
            loadingProgress += Time.deltaTime / loadingDuration;
            progressBar.value = loadingProgress;

            // 更新文字提示
            int percent = Mathf.RoundToInt(loadingProgress * 100);
            loadingText.text = $"Loading... {percent}%";

            yield return null;
        }

        // 加载完成，立即切换到目标场景
        SceneManager.LoadScene(sceneIndex);
    }
}