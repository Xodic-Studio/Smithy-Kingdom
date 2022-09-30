using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManagerr : Singleton<SoundManagerr>
{
    [SerializeField] private Sound[] sounds;

    [SerializeField] AudioMixer audioMixer;
    private void Awake()
    {
        DontDestroyOnLoad(this);

    }
    
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public void UpdateMasterVolume()
    {
        float sliderValue = masterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    }
    
    public void UpdateMusicVolume()
    {
        float sliderValue = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }
    
    public void UpdateSFXVolume()
    {
        float sliderValue = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }
    
    [Serializable]
    struct Sound
    {
        public string SoundName;
        public AudioClip[] _AudioClip;
        public AudioMixerGroup mixerGroup;
        public bool Loop;
        public bool IsRandom;
        [Range(0f, 1f)] public float Pan;

        [HideInInspector] public AudioSource AudioSource;
    }

    
    private void Start()
    {
        //set default
        for (int i = 0; i < sounds.Length; i++)
        {
            Sound _sounds = sounds[i];
            
            sounds[i].AudioSource = gameObject.AddComponent<AudioSource>();
            sounds[i].AudioSource.loop = _sounds.Loop;
            sounds[i].AudioSource.clip = _sounds._AudioClip[0];
            sounds[i].AudioSource.volume = 1;
            sounds[i].AudioSource.panStereo = _sounds.Pan;
            sounds[i].AudioSource.outputAudioMixerGroup = _sounds.mixerGroup;
        }
    }

    public void PlaySound(string soundName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            Sound _sounds = sounds[i];
            
            if (_sounds.SoundName == soundName)
            {
                //random
                if (_sounds.IsRandom)
                {
                    sounds[i].AudioSource.PlayOneShot
                            (_sounds._AudioClip[Random.Range(0, _sounds._AudioClip.Length)],1);
                }
                else
                {
                    sounds[i].AudioSource.Play();
                }

            }
        }
    }
}