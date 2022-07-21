
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : Singleton<SoundManager>
{
    public SoundDatabase soundDatabase;

    // Volumes
    private float fixedVolume = 0.1f;
    public float masterVolume = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    // Audio players components.
    public AudioSource EffectsSource;

    public AudioSource MusicSource;

    // Random pitch adjustment range.
    public float LowPitchRange = .95f;

    public float HighPitchRange = 1.05f;
    // Singleton instance.

    private void Awake()
    {
        DontDestroyOnLoad(this);

    }

    private void Start()
    {
        UpdateMusicVolume();
    }

    /// <summary>
    /// Play a single sound clip.
    /// </summary>
    /// <param name="clip">Music Clip to play</param>
    public void PlayOneShot(AudioClip clip)
    {
        EffectsSource.PlayOneShot(clip, masterVolume * sfxVolume);
    }

    /// <summary>
    /// Play a Music Clip Through the Music Source.
    /// </summary>
    /// <param name="clip">Music Clip</param>
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    /// <summary>
    /// Play a random sound clip from specified sfx.
    /// </summary>
    /// <param name="clips">SFX Array yo play</param>
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        EffectsSource.PlayOneShot(clips[randomIndex], masterVolume * sfxVolume);
    }

    public void UpdateMusicVolume()
    {
        MusicSource.volume = fixedVolume * masterVolume * musicVolume;
    }
    
}

