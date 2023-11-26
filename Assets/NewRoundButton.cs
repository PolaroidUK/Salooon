using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRoundButton : MonoBehaviour
{
    private SpriteRenderer sp;
    private BoxCollider2D col;
    private SwimmerManager sw;
    
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sw = FindObjectOfType<SwimmerManager>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        sp.enabled = sw.gameEnded;
        col.enabled = sw.gameEnded;
    }
}
