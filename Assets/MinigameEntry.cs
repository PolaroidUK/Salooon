using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameEntry : MonoBehaviour
{
    public int sceneIdForMinigame;
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneIdForMinigame);
    }
}
