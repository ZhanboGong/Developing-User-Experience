using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public GameObject homePageCanvas; 
    public GameObject startPageCanvas;
    
    void Start () {
        camera2.enabled = false;
        if (startPageCanvas != null) startPageCanvas.SetActive(false);
    }

    public void SwitchToCamera()
    {
        camera2.enabled = true;
        camera1.enabled = false;
        if(homePageCanvas != null) homePageCanvas.SetActive(false);
        if(startPageCanvas != null) startPageCanvas.SetActive(true);
    }
}