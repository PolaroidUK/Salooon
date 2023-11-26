using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Outside");
    }
}
