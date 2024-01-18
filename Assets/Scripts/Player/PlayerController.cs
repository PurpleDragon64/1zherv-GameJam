using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;
    public Vector3 spawnPoint = Vector3.zero;

    private void Awake()
    {
        // subscribe to GameStateChanged event
        GameManager.OnGameStateChanged += OnGameStateChange;
    }

    private void OnDestroy()
    {
        // unsubscribe from GameStateChanged event
        GameManager.OnGameStateChanged -= OnGameStateChange;
    }


    private void OnGameStateChange(GameState state)
    {
        if (state == GameState.Playing)
        {
            // enable movement
            movement.moveLocked = false;
            // move player to starting position
            transform.position = spawnPoint;
        }
        else
        {
            // disable movement
            movement.moveLocked = true;
        }
    }
}
