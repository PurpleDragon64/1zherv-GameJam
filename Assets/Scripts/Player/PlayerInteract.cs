using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public int interactRange;
    public LayerMask interactibleLayer;

    void Update()
    {
        // check for interactibles in range
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, interactRange, interactibleLayer);
        
        for(int i = 0; i < rangeCheck.Length; i++) {
            // for each interactible in range, call focus
            IInteractible interactible = rangeCheck[i].GetComponent<IInteractible>();
            interactible.InteractibleOnFocus();
		}
    }

    
}
