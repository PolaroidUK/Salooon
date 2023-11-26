using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    public Item item;
    public GameObject highlight;

    private void Start()
    {
        highlight = transform.GetChild(0).gameObject;
    }
}
