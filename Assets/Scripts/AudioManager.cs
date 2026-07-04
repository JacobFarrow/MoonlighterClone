using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sound Effects")]
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip pickupSound;
    public AudioClip bellSound;

    [Header("Music")]
    public AudioClip dungeonMusic;
    public AudioClip shopMusic;
    [Range(0f, 1f)] public float musicVolume = 0.4f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;

            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip target = null;

        if (scene.name == "DungeonScene")
            target = dungeonMusic;
        else if (scene.name == "ShopScene")
            target = shopMusic;

        if (target == null)
        {
            musicSource.Stop();
            return;
        }

        if (musicSource.clip == target && musicSource.isPlaying)
            return;

        musicSource.clip = target;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayAttack() => PlaySFX(attackSound);
    public void PlayHit() => PlaySFX(hitSound);
    public void PlayPickup() => PlaySFX(pickupSound);
    public void PlayBell() => PlaySFX(bellSound);
}