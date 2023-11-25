using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverHandler : MonoBehaviour
{
    public GameObject hoveredGO;
    public enum HoverState { HOVER, NONE };
    public HoverState hover_state = HoverState.NONE;

    public TMP_Text hoverText;
    bool textShown = false;

    private void Start()
    {
        hoverText.enabled = false;
    }

    public void OnHoverOver(string text)
    {
        hoverText.text = text;
        textShown = true;
        hoverText.enabled = true;
    }

    public void OnHoverExit()
    {
        textShown = false;
        hoverText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (textShown)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 textPos = new Vector3(mousePos.x + hoverText.preferredWidth / 2, mousePos.y + hoverText.preferredHeight / 2, 0f);
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(textPos);
            screenPos.z = 0;
            hoverText.transform.position = screenPos;
        }
    }
}
