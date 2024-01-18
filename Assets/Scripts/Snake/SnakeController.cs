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
        if (state == GameState.Playing)
        {
            // disable snake movement
            snakeAI.agent.isStopped = false;
            // move snake to starting position
            transform.position = snakeAI.waypoints[0].position;
            // set first waypoint as current active
            snakeAI.agent.SetDestination(snakeAI.waypoints[1].position);
        }
        else
        {
            // disable snake movement
            snakeAI.agent.isStopped = true;
        }
    }
}
