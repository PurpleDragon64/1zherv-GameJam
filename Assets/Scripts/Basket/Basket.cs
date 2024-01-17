using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketInteract : MonoBehaviour, IInteractible
{
    private bool playerInside;
    private bool focused;

    void Start() {
        playerInside = false;
        focused = false;
    }

    public void Intble_Focus() { 
        if(!focused) { 
			focused = true;
		}
    }

    public void Intble_Unfocus() {
        if(focused) { 
			focused = false;
		}
    }

    public bool Intble_IsFocused() {
        return focused;
    }

    public void Interact(GameObject Interacter) { 
        if(playerInside) {
            // player is inside, let him out	
            
		}
        else {
            // player is not inside, let him in	

            // set interacters layer to ignore raycast layer
            Interacter.layer = 2;
            

		}

        print(Interacter + " interacted with basket");
    }

    public bool Intble() {
        return true;
    }
}
