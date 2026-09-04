using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
   
    public AudioSource sfxSource;
    public AudioSource musicSource;
    [SerializeField] private float masterVolume = 0.5f;

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

    public void PlaySFX(AudioClip clip, float volume = 1)
    {
        if (clip == null) return;
        sfxSource.volume = Mathf.Clamp01(volume) * masterVolume;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        //edge case handling to avoid repeating the music track.
        if (musicSource.clip == clip) return; 

        musicSource.clip = clip;
        musicSource.volume = masterVolume;
        musicSource.Play();
    }

    //Call from other scripts to stop music.
    public void StopMusic()
    {
        musicSource.Stop();
    }
}


