using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour
{
    public UnityEvent onPress;

    private void OnMouseDown()
    {
        onPress.Invoke();
    }
}
