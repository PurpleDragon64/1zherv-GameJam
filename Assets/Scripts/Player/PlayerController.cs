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
        GameManager.OnGameStateChanged += OnGameStateChange;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChange;
    }


    private void OnGameStateChange(GameState state)
    {
        if (state == GameState.Playing)
        {
            movement.moveLocked = false;
            transform.position = spawnPoint;
        }
        else
        {
            movement.moveLocked = true;
        }
    }
}
