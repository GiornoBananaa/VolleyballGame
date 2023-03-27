using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private Slider _soundSlider;
    [SerializeField]private Slider _musicSlider;
    [SerializeField]private AudioSource _musicSource;
    [SerializeField]private AudioSource _soundsSource;
    private float _soundsVolume;
    private float _musicVolume;

    void Start()
    {
        DontDestroyOnLoad(_musicSource.gameObject);
        DontDestroyOnLoad(_soundsSource.gameObject);

        _soundsVolume = PlayerPrefs.GetFloat("Sounds", _soundsSource.volume);
        _musicVolume = PlayerPrefs.GetFloat("Music", _musicSource.volume);

        _musicSource.volume = _musicVolume;
        _soundsSource.volume = _soundsVolume;
        _musicSlider.value = _musicVolume;
        _soundSlider.value = _soundsVolume;
    }

    public void ChangeMusicVolume()
    {
        _musicSource.volume = _musicSlider.value;
        PlayerPrefs.SetFloat("Music", _musicVolume);
    }

    public void ChangeSoundsVolume()
    {
        _soundsSource.volume = _soundSlider.value;
        PlayerPrefs.SetFloat("Sounds", _soundsVolume);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("Sounds", _soundsVolume);
        PlayerPrefs.SetFloat("Music", _musicVolume);
        PlayerPrefs.Save();
    }
}
