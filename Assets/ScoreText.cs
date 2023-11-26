using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private Image background;

    private void Start()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
        background = GetComponent<Image>();
    }

    public void ShowScore(float score)
    {
        scoreText.enabled = true;
        scoreText.text = score.ToString();
        background.enabled = true;
    }

    public void Hide()
    {
        scoreText.enabled = false;
        background.enabled = false;
    }
}
