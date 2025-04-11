using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LoadManager : MonoBehaviour
{
    // ����UI���
    public Slider progressBar;
    public TextMeshProUGUI loadingText;
    public Canvas loadingCanvas;

    // ���س���������
    private int sceneIndex;

    // ���ؽ���
    private float loadingProgress = 0f;

    // ���ض����ĳ���ʱ�䣨�룩
    public float loadingDuration = 3f;

    private void Start()
    {
        // ��ʼ������CanvasΪ����״̬
        loadingCanvas.gameObject.SetActive(false);
    }

    // ��ʼ���س���
    public void LoadScene(int index)
    {
        sceneIndex = index;

        // ��ʾ����Canvas
        loadingCanvas.gameObject.SetActive(true);

        // ģ����ؽ���
        StartCoroutine(SimulateLoading());
    }

    // ģ����ؽ���
    private IEnumerator SimulateLoading()
    {
        // ���ü��ؽ���
        loadingProgress = 0f;
        progressBar.value = 0f;
        loadingText.text = "Loading... 0%";

        // ģ����ض���
        while (loadingProgress < 1f)
        {
            // ƽ�����½���
            loadingProgress += Time.deltaTime / loadingDuration;
            progressBar.value = loadingProgress;

            // ����������ʾ
            int percent = Mathf.RoundToInt(loadingProgress * 100);
            loadingText.text = $"Loading... {percent}%";

            yield return null;
        }

        // ������ɣ������л���Ŀ�곡��
        SceneManager.LoadScene(sceneIndex);
    }
}