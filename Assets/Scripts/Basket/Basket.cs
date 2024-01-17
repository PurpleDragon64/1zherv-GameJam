using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketInteract : MonoBehaviour, IInteractible
{
    bool playerInside;

    void Start() {
        playerInside = false; 
    }

    public void InteractibleOnFocus() { 
        print("basket in focus");
    }

    public void InteractibleOnUnfocus() { 

    }

    public void Interact(GameObject Interacter) { 
        if(playerInside) {
            // player is inside, let him out	
            
		}
        else {
            // player is not inside, let him in	

            // set interacters layer to ignore raycast layer
            // Interacter.layer = 2;
		}

        print(Interacter + " interacted with basket");
    }

    public bool Interactible() {
        return true;
    }
}
