using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour
{
    private GameObject prefab;

    private void Awake()
    {
        // Load snake prefab from Assets
        prefab = Resources.Load<GameObject>("Snake");

        // Subscribe to Game State changed event
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Intro)
        {
            // Load transform from every child
            // Note: first waypoint is the parent (this GO)
            Transform[] waypoints = GetComponentsInChildren<Transform>();

            // Spawn snake and fill his waypoints
            GameObject snake = Instantiate(prefab, transform);
            snake.GetComponent<SnakeAI>().waypoints = waypoints;
        }
    }

}
