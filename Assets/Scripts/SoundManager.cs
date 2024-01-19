using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioSource audio;
    [SerializeField]
    private AudioClip clipDeath;

    private void Awake()
    {
        // subscribe to GameStateChanged event
        GameManager.OnGameStateChanged += OnGameStateChange;
        audio = GetComponent<AudioSource>();
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
        else
        {
            audio.Play();
        }
    }
}
