using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Sound Manager is null");

            return _instance;
        }
    }

    private AudioSource audio;
    [SerializeField] private AudioClip clipDeath;
    [SerializeField] private AudioClip clipBasketIn;
    [SerializeField] private AudioClip clipBasketOut;
    [SerializeField] private AudioClip clipDash;
    [SerializeField] private AudioClip clipWin;

    public void PlayBasketIn() {
        audio.PlayOneShot(clipBasketIn,5);
    }

    public void PlayBasketOut() { 
        audio.PlayOneShot(clipBasketOut,5);
    }

    public void PlayDash() { 
        audio.PlayOneShot(clipDash);
    }

    private void Awake()
    {
        // subscribe to GameStateChanged event
        GameManager.OnGameStateChanged += OnGameStateChange;
        audio = GetComponent<AudioSource>();
        _instance = this;
    }

    private void OnDestroy()
    {
        // unsubscribe from GameStateChanged event
        GameManager.OnGameStateChanged -= OnGameStateChange;
    }


    private void OnGameStateChange(GameState state)
    {
        if (state == GameState.Death)
        {
            audio.Stop();
            audio.PlayOneShot(clipDeath);
        }
        else if(state == GameState.Win) { 
            audio.Stop();
            audio.PlayOneShot(clipWin);
		}
        else
        {
            audio.Play();
        }
    }
}
