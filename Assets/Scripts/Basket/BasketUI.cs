using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketUI : MonoBehaviour
{
    bool hidden;

    private void Start()
    {
        hidden = true;
	    gameObject.SetActive(false);
    }

    public void ShowBubble()
    {
        if(hidden) {
            hidden = false;
            gameObject.SetActive(true);
		}
        
    }

    public void HideBubble()
    { 
        if(!hidden) {
            hidden = true;	
            gameObject.SetActive(false);
		}
    }

    private void EaseInFadeIn() { 
         
    }
}
