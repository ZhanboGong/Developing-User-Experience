using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;  
    public Button musicButton;
    public Sprite playIcon;         
    public Sprite pauseIcon;        
    
    private bool isPlaying = true;
    
    void Start()
    {
     
        audioSource.Play();
        musicButton.image.sprite = pauseIcon;
        
       
        musicButton.onClick.AddListener(ToggleMusic);
    }
    
    void ToggleMusic()
    {
        isPlaying = !isPlaying;
        
        if(isPlaying)
        {
            audioSource.Play();
            musicButton.image.sprite = pauseIcon;
        }
        else
        {
            audioSource.Pause();
            musicButton.image.sprite = playIcon;
        }
    }
}
