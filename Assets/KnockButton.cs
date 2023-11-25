using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockButton : MonoBehaviour
{
    private SpriteRenderer sp;
    [SerializeField] private Sprite normal,grey;
    private SwimmerManager sw;
    
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sw = FindObjectOfType<SwimmerManager>();
    }

    void Update()
    {
        sp.sprite = sw.firstRound ? grey : normal;
    }
}
