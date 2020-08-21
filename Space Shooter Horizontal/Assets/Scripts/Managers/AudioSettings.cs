using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    const string SOUND_KEY = "sound";
    const string MUSIC_KEY = "music";

    [SerializeField]
    private Slider _soundSlider;
    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    private AudioMixer _masterMixer;

    private float _sound;
    private float _music;

    private void Start()
    {
        _sound = PlayerPrefs.GetFloat(SOUND_KEY);
        _masterMixer.SetFloat("VolumeSound", _sound);
        _music = PlayerPrefs.GetFloat(MUSIC_KEY);
        _masterMixer.SetFloat("VolumeMusic", _music);

        _soundSlider.value = _sound;
        _musicSlider.value = _music;
    }

    private void Update()
    {
        if (_sound != _soundSlider.value)
        {
            _sound = _soundSlider.value;
            _masterMixer.SetFloat("VolumeSound", _sound);
            PlayerPrefs.SetFloat(SOUND_KEY, _sound);
        }

        if (_music != _musicSlider.value)
        {
            _music = _musicSlider.value;
            _masterMixer.SetFloat("VolumeMusic", _music);
            PlayerPrefs.SetFloat(MUSIC_KEY, _music);
        }
    }
}
