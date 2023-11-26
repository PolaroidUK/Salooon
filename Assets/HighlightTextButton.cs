using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class HighlightTextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color hoverColor;
    public Color normalColor;
    private TMP_Text theText;

    private void Start()
    {
        theText = GetComponentInChildren<TMP_Text>();
        theText.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = hoverColor; //Or however you do your color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = normalColor; //Or however you do your color
    }
}