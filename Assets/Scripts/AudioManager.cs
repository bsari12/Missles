using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip explosionSound;

    private bool isMuted = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSoundSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (musicSource.clip == null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayExplosion()
    {
        sfxSource.PlayOneShot(explosionSound);
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        ApplySoundSettings();
    }

    private void ApplySoundSettings()
    {
        AudioListener.volume = isMuted ? 0f : 1f;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadSoundSettings()
    {
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        AudioListener.volume = isMuted ? 0f : 1f;
    }

    public bool IsMuted()
    {
        return isMuted;
    }
}