using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource engineSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip engineSound;
    public AudioClip explosionSound;

    private bool isMuted = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();

        engineSource.clip = engineSound;
        engineSource.loop = true;
        engineSource.Play();
    }

    public void PlayExplosion()
    {
        sfxSource.PlayOneShot(explosionSound);
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1;
    }

    public bool IsMuted()
    {
        return isMuted;
    }
}