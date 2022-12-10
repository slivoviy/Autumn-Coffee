using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider ambSlider;

    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";
    public const string MIXER_AMB = "AmbVolume";

    void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        ambSlider.onValueChanged.AddListener(SetAmbVolume);
    }
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
        ambSlider.value = PlayerPrefs.GetFloat(AudioManager.AMB_KEY, 1f);
    }
    void OnDisable()
    {
        PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.GetFloat(AudioManager.SFX_KEY, sfxSlider.value);
        PlayerPrefs.GetFloat(AudioManager.AMB_KEY, ambSlider.value);
    }
    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }
    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }
    void SetAmbVolume(float value)
    {
        mixer.SetFloat(MIXER_AMB, Mathf.Log10(value) * 20);
    }
}
