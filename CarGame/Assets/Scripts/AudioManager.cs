using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgMusicSource;
    public AudioSource[] sfxSources;

    private float bgMusicVolume = 1f;
    private float sfxVolume = 1f;
    private float bgMusicTime = 0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceleneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        LoadVolumeSettings();
    }

    private void OnSceleneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignAudioSources();

        if (bgMusicSource != null)
        {
            bgMusicSource.time = bgMusicTime;
            if (!bgMusicSource.isPlaying)
            {
                bgMusicSource.Play();
            }
        }
        ApplyVolumeSettigns();
    }

    private void AssignAudioSources()
    {
        bgMusicSource = GameObject.Find("bg")?.GetComponent<AudioSource>();

        if(bgMusicSource != null)
        {
            bgMusicSource.loop = true;
        }

        AudioSource[] allAudioSource = FindObjectsOfType<AudioSource>();

        sfxSources = System.Array.FindAll(allAudioSource, source => source != bgMusicSource);
    }
    private void LoadVolumeSettings()
    {
        bgMusicVolume = PlayerPrefs.GetFloat("BGMusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    private void ApplyVolumeSettigns()
    {
        if(bgMusicSource != null)
        {
            bgMusicSource.volume = bgMusicVolume;
        }

        foreach(var sfx in sfxSources)
        {
            if (sfx != null)
            {
                sfx.volume = sfxVolume;
            }
        }
    }
    public void SetBGMusicVolume(float volume)
    {
        bgMusicVolume = volume;
        PlayerPrefs.SetFloat("BGMusicVolume", bgMusicVolume);
        ApplyVolumeSettigns();
    }
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        ApplyVolumeSettigns();
    }
    private void Update()
    {
        if(bgMusicSource != null && bgMusicSource.isPlaying)
        {
            bgMusicTime = bgMusicSource.time;
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("BGMusicVolume", bgMusicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }
}
