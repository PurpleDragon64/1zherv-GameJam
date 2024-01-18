using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public SnakeAI snakeAI;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Playing)
        {
            snakeAI.agent.isStopped = false;
            transform.position = snakeAI.waypoints[0].position;
            snakeAI.agent.SetDestination(snakeAI.waypoints[1].position);
        }
        else
        {
            snakeAI.agent.isStopped = true;
        }
    }
}
