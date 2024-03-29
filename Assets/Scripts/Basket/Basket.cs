using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasketInteract : MonoBehaviour, IInteractible
{
    [SerializeField]
    private bool playerInside;
    private bool focused;
    private Vector2 direction;
    public BubbleHintUI ui;
    public Transform basketLid;

    void Start() {
        playerInside = false;
        focused = false;
        basketLid.localPosition = new Vector3(.25f, -.25f);
    }

    void Update() { 
        if(playerInside) {
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");
            direction = direction.normalized;

            if(direction == Vector2.zero) {
                direction = Vector2.right; 
		    }

        }
    }

    public void Intble_Focus() { 
        if(!playerInside) {
			focused = true;
            ui.ShowBubble();
			basketLid.localPosition = new Vector3(.4f, -.4f);
		}
    }

    public void Intble_Unfocus() {
		focused = false;
	    ui.HideBubble();
		basketLid.localPosition = new Vector3(.25f, -.25f);
    }

    public bool Intble_IsFocused() {
        return focused;
    }

    public void Interact(GameObject Interacter) { 
        if(playerInside) {
            // player is inside, let him out	
            // set interacters layer to player layer
            Interacter.layer = 10;
            Interacter.GetComponent<PlayerMovement>().moveLocked = false;
            Interacter.transform.position = transform.position + new Vector3(direction.x, direction.y, 0);
            Interacter.GetComponent<SpriteRenderer>().enabled = true;
            Interacter.GetComponent<CircleCollider2D>().enabled = true;
            playerInside = false;

            // play sound effect
            SoundManager.Instance.PlayBasketIn();
		}
        else {
            // player is not inside, let him in	
            // set interacters layer to ignore raycast layer
            Interacter.layer = 2;
            Interacter.GetComponent<PlayerMovement>().moveLocked = true;
            Interacter.transform.position = transform.position;
            Interacter.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Interacter.GetComponent<SpriteRenderer>().enabled = false;
            Interacter.GetComponent<CircleCollider2D>().enabled = false;

            playerInside = true;

            // play sound effect
            SoundManager.Instance.PlayBasketOut();

            // dont show ui bubble when inside
            ui.HideBubble();
            // close the lid
			basketLid.localPosition = Vector3.zero;

		}

        print(Interacter + " interacted with basket");
    }

    public bool Intble() {
        return true;
    }
}
