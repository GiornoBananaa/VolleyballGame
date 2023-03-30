using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private Slider _soundSlider;
    [SerializeField]private Slider _musicSlider;
    [SerializeField] private GameObject _musicSourcePrefab;
    [SerializeField] private GameObject _soundsSourcePrefab;

    private static AudioSource _musicSource;
    private static AudioSource _soundsSource;
    static bool AudioBegin = false;
    private float _soundsVolume;
    private float _musicVolume;

    private void Awake()
    {
        if (!AudioBegin)
        {
            GameObject source1 = Instantiate(_musicSourcePrefab);
            GameObject source2 = Instantiate(_soundsSourcePrefab);

            _musicSource = source1.GetComponent<AudioSource>();
            _soundsSource = source2.GetComponent<AudioSource>();

            _soundsSource.playOnAwake = false;

            DontDestroyOnLoad(source1);
            DontDestroyOnLoad(source2);
            AudioBegin = true;
        }
    }

    void Start()
    {
        _soundsVolume = PlayerPrefs.GetFloat("Sounds", _soundsSource.volume);
        _musicVolume = PlayerPrefs.GetFloat("Music", _musicSource.volume);

        _musicSource.volume = _musicVolume;
        _soundsSource.volume = _soundsVolume;
        _musicSlider.value = _musicVolume;
        _soundSlider.value = _soundsVolume;
    }

    public void ChangeMusicVolume()
    {
        _musicVolume = _musicSlider.value;
        _musicSource.volume = _musicVolume;
        PlayerPrefs.SetFloat("Music", _musicVolume);
        PlayerPrefs.Save();
    }

    public void ChangeSoundsVolume()
    {
        _soundsVolume = _soundSlider.value;
        _soundsSource.volume = _soundsVolume;
        PlayerPrefs.SetFloat("Sounds", _soundsVolume);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("Sounds", _soundsVolume);
        PlayerPrefs.SetFloat("Music", _musicVolume);
        PlayerPrefs.Save();
    }

    public float SoundVolume()
    {
        return _soundsVolume;
    }
    public float MusicVolume()
    {
        return _soundsVolume;
    }
}
