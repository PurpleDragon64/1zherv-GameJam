using UnityEngine;

public interface IInteractible {
    void Interact(GameObject Interacter);
    bool Interactible();
    void InteractibleOnFocus();
    void InteractibleOnUnfocus();
}