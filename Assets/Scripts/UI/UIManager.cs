using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject DeathScreen;
    [SerializeField] GameObject WinScreen;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChange;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChange;
    }


    private void OnGameStateChange(GameState state) {
        switch (state)
        {
            case GameState.Intro:
                MainMenu.SetActive(true);
                DeathScreen.SetActive(false);
                WinScreen.SetActive(false);
                break;
            case GameState.Playing:
                MainMenu.SetActive(false);
                DeathScreen.SetActive(false);
                WinScreen.SetActive(false);
                break;
            case GameState.Death:
                MainMenu.SetActive(false);
                WinScreen.SetActive(false);
                StartCoroutine(ShowDeathScreen());
                break;
            case GameState.Win:
                MainMenu.SetActive(false);
                DeathScreen.SetActive(true);
                WinScreen.SetActive(true);
                break;
        }
    }

    IEnumerator ShowDeathScreen() {
        yield return new WaitForSecondsRealtime(.5f);
		DeathScreen.SetActive(true);
    }
}
