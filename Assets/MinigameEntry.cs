using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameEntry : MonoBehaviour
{
    public int sceneIdForMinigame;
    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneIdForMinigame);
    }
}
