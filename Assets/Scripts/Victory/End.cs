using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class End : MonoBehaviour, IInteractible
{
    private bool focused;
    public BubbleHintUI ui;

    void Start()
    {
        focused = false;
    }

    public void Intble_Focus()
    {
        focused = true;
        ui.ShowBubble();
    }

    public void Intble_Unfocus()
    {
        focused = false;
        ui.HideBubble();
    }

    public bool Intble_IsFocused()
    {
        return focused;
    }

    public void Interact(GameObject Interacter)
    {
        GameManager.Instance.UpdateGameState(GameState.Win);
    }

    public bool Intble()
    {
        return true;
    }
}
