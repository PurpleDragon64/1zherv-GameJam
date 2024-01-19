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

    // signature of functins that hangle GameState changes
    public delegate void StateChanged(GameState newState);
    // event, which broadcasts that GameState change happened
    public static event StateChanged OnGameStateChanged;


    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        // Game start in state Intro
        UpdateGameState(GameState.Intro);
    }

    // Update is called once per frame
    void Update()
    {
        // Handle user input for various states
        if (state == GameState.Intro)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UpdateGameState(GameState.Playing);
            }
        }
        if (state == GameState.Death || state == GameState.Win)
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
                HandleStateWin();
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
        print("you lost");
        Time.timeScale = 0;
    }

    private void HandleStateWin()
    {
        print("you won!!");
    }
}

public enum GameState {
    Intro,      // show start screen, wait for user input
    Playing,    // player and enemies can move
    Death,      // player collided with enemy, freeze, show restart prompt and wait for input
    Win         // player reached final destination, show winning screen
};