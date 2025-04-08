using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public GameObject homePageCanvas; // HomePage 画布
    public GameObject startPageCanvas; // StartPage 画布
    
    void Start () {
        // 确保第二个摄像机一开始是禁用的
        camera2.enabled = false;
        // 确保 StartPage 画布一开始是隐藏的
        if (startPageCanvas != null) startPageCanvas.SetActive(false);
    }

    public void SwitchToCamera()
    {
        // 切换摄像机
        camera2.enabled = true;
        camera1.enabled = false;
        
        // 切换画布的可见性
        if(homePageCanvas != null) homePageCanvas.SetActive(false);
        if(startPageCanvas != null) startPageCanvas.SetActive(true);
    }
}