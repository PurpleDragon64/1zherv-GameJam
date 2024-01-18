using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public SnakeAI snakeAI;

    private void Awake()
    {
        // subscribe to GameStateChanged event
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        // unsubscribe from GameStateChanged event
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Death)
        {
            // disable snake movement
            snakeAI.EnableMovement(false);
        }
    }
}
