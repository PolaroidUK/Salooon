using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOver : MonoBehaviour
{
    HoverHandler hoverHandler;
    public string hoverText;

    private void Start()
    {
        hoverHandler = FindObjectOfType<HoverHandler>();
    }

    void OnMouseOver()
    {
        hoverHandler.OnHoverOver(hoverText);
    }

    void OnMouseExit()
    {
        hoverHandler.OnHoverExit();
    }
}
