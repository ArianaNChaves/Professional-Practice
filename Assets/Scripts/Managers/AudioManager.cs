using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSO audioData;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicSource, sfxSource;

    private string _lastSong;
    private const string MixerMusic = "MusicVolume";
    private const string MixerSFX = "SFXVolume";
    private const string MixerUi = "UiVolume";
    

    public void PlayMusic(string musicName)
    {
        Sound sound = Array.Find(musicSounds, x => x.soundName == musicName);
        if (sound == null)
        {
            Debug.LogError("Sound not found");
        }
        else
        {
            if (musicName == _lastSong) return;
            _lastSong = sound.soundName;
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }
    public void PlayEffect(string effectName)
    {
        Sound effect = Array.Find(sfxSounds, x => x.soundName == effectName);
        if (effect == null)
        {
            Debug.LogError("Effect not found");
        }
        else
        {
            sfxSource.PlayOneShot(effect.clip);
        }
    }
    public void MusicVolume(float volume)
    {
        audioData.MusicVolume = volume;
        audioMixer.SetFloat(MixerMusic, Mathf.Log10(volume) * 20);
    }
    public void SfxVolume(float volume)
    {
        audioData.SFXVolume = volume;
        audioMixer.SetFloat(MixerSFX, Mathf.Log10(volume) * 20);
    }
    
    public void UiVolume(float volume)
    {
        audioData.UIVolume = volume;
        audioMixer.SetFloat(MixerUi, Mathf.Log10(volume) * 20);
    }

    public float GetMusicVolume()
    {
        return audioData.MusicVolume;
    }

    public float GetSFXVolume()
    {
        return audioData.MusicVolume;
    }
    public float GetUiVolume()
    {
        return audioData.MusicVolume;
    }
    public void StopMusic()
    {
        _lastSong = "";
        musicSource.Stop();
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    
    
    [System.Serializable]
    public class Sound
    {
        public string soundName;
        public AudioClip clip;
    }
}
