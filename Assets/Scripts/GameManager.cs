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

    public enum GameState {Intro, Playing, Death, Win};
    public GameState state;

    public delegate void StateChanged(GameState newState);
    public static event StateChanged OnGameStateChanged;


    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        state = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.Intro:
                break;
            case GameState.Playing:
                break;
            case GameState.Death:
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
}
