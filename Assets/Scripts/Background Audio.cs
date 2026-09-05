using UnityEngine;
using System.Collections;
public class BackgroundAudio : MonoBehaviour
{
    public AudioClip arcadeAmbience;
    public AudioClip backgroundSong; 
    private AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       if(audioManager == null)
        {
            audioManager = AudioManager.Instance;
          
        }
        audioManager.PlayMusic(backgroundSong);
        StartCoroutine(PlayArcadeAmbience());
    }

    // Update is called once per frame
    IEnumerator PlayArcadeAmbience()
    {
        while(true)
        {
            audioManager.PlaySFX(arcadeAmbience);
            yield return new WaitForSeconds(60);
        }
    }
}
