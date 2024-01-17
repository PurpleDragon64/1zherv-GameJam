using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public int interactRange;
    public LayerMask interactibleLayer;
    private IInteractible lastNearestIntble;
    private IInteractible nearestIntble;

    private void Start()
    {
        lastNearestIntble = null;
        nearestIntble = null;
    }

    void Update()
    {
        nearestIntble = getNearestIntble();

        if(nearestIntble != null) { 
			// focus the nearest interactible
            if(lastNearestIntble != null) { 
				if(!nearestIntble.Intble_IsFocused() && lastNearestIntble.Intble_IsFocused()) {
					// switch focus to the new nearest interactible
					lastNearestIntble.Intble_Unfocus();
			    }
		    }

			lastNearestIntble = nearestIntble;
			nearestIntble.Intble_Focus();

            // interact action
			if(Input.GetKeyDown(KeyCode.E)) {
                nearestIntble.Interact(this.gameObject);
			}
		}
        else {
            // no interactible in range, unfocus if any interactible is focused	
            if(lastNearestIntble != null) { 
			    lastNearestIntble.Intble_Unfocus();
		    }
		}
    }

    private IInteractible getNearestIntble() { 
        // check for interactibles in range
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, interactRange, interactibleLayer);
        float minDist = Mathf.Infinity;
        IInteractible nearestIntble = null;
        
        for(int i = 0; i < rangeCheck.Length; i++) {
            // get the nearest interactible
            Transform target = rangeCheck[i].transform;
            Vector2 dir2target = target.position - transform.position;
            float sqrDist = dir2target.sqrMagnitude;

            if(sqrDist < minDist) {
                minDist = dir2target.sqrMagnitude;
                nearestIntble = rangeCheck[i].GetComponent<IInteractible>();
		    }
		}

        return nearestIntble;

    }

    
}
