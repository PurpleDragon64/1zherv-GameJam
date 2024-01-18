using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is null");

            return _instance;
        }
    }

  
    public GameState state;

    public delegate void StateChanged(GameState newState);
    public static event StateChanged OnGameStateChanged;


    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.Intro);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Intro)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                UpdateGameState(GameState.Playing);
            }
        }
        if (state == GameState.Death)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                UpdateGameState(GameState.Playing);
            }
        }
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.Intro:
                HandleStateIntro();
                break;
            case GameState.Playing:
                HandleStatePlaying();
                break;
            case GameState.Death:
                HandleStateDeath();
                break;
            case GameState.Win:
                break;
            default:
                break;
        }

        if (OnGameStateChanged != null)
        {
            OnGameStateChanged(state);
        }
    }

    private void HandleStateIntro()
    {
    }

    private void HandleStatePlaying()
    {
        Time.timeScale = 1;
    }

    private void HandleStateDeath()
    {
        print("Died");
        Time.timeScale = 0;
    }
}

public enum GameState {
    Intro,
    Playing,
    Death,
    Win
};