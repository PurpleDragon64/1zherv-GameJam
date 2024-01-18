using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour
{
    private GameObject prefab;

    private Transform[] waypoints;
    private GameObject snake;

    private void Awake()
    {
        // Load snake prefab from Assets
        prefab = Resources.Load<GameObject>("Snake");

        // subscribe to GameStateChanged event
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        // unsubscribe from GameStateChanged event
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void Start()
    {
        // Load transform of every child (waypoint)
        // Note: first waypoint in array is the parent (this GO)
        waypoints = GetComponentsInChildren<Transform>();
        snake = null;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Playing)
        {
            KillSnake();
            SpawnSnake();
        }
    }

    private void KillSnake()
    {
        // in first start, no snake is present
        if (snake != null)
        {
            Destroy(snake);
        }
    }

    private void SpawnSnake()
    {
        // Spawn snake and fill his waypoints and set chase target
        snake = Instantiate(prefab, transform);
        SnakeAI snakeAI = snake.GetComponent<SnakeAI>();
        snakeAI.waypoints = waypoints;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        snakeAI.chaseTarget = player;
    }

}
