using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldSpaceUI : MonoBehaviour
{
    VisualElement root;
    VisualTreeAsset bubbleAsset;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        bubbleAsset = Resources.Load<VisualTreeAsset>("basket-bubble");
    }

    public VisualElement InstantiateBubble(Vector3 position)
    {
        VisualElement bubble = bubbleAsset.Instantiate();
        root.Add(bubble);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        bubble.style.left = screenPos.x - (bubble.layout.width / 2);
        bubble.style.top = Screen.height - screenPos.y;

        print("added bubble " + bubble);
        return bubble;
    }

    public void ShowBubble(VisualElement bubble) {
        bubble.RemoveFromClassList("disabled");         
    }

    public void HideBubble(VisualElement bubble)
    {
        bubble.AddToClassList("disabled");
    }
}
