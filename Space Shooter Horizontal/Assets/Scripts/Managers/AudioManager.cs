using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _menuMusic;
    [SerializeField]
    private AudioSource _gameMusic;

    public static AudioManager _instance;


    private void Start()
    {
        if (_instance == null) 
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && !_menuMusic.isPlaying)
        {
            _menuMusic.Play();
            _gameMusic.Stop();
        }
        else if(SceneManager.GetActiveScene().buildIndex != 0 && !_gameMusic.isPlaying)
        {
            _gameMusic.Play();
            _menuMusic.Stop();
        }
    }

}