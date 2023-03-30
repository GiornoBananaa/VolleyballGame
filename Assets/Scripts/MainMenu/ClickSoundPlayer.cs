using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSoundPlayer : MonoBehaviour
{
    [SerializeField]private AudioManager _audioManager;
    private AudioSource _clickSource;

    private void Start()
    {
        _clickSource = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        _clickSource.volume = _audioManager.SoundVolume();
        _clickSource.Play();
    }
}
