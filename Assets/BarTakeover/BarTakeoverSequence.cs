using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BarTakeoverSequence : MonoBehaviour
{
    [SerializeField]
    private List<SequenceSegment> sequenceSegments;
    public int currentSegmentIndex;

    public GameObject dialogueButton;

    [Serializable]
    public class SequenceSegment
    {
        [HideInInspector]
        public string name = "Segment";
        public GameObject barPatron;
        public GameObject drink;
        public GameObject textBox;
        public GameObject dialogueBox;


        public List<GameObject> GetGOs()
        {
            return new List<GameObject> { barPatron, drink, textBox, dialogueBox };
        }
    }

    void Start()
    {
        currentSegmentIndex = 0;
        BarSequence();
    }

    public void BarSequence()
    {
        DealWithBartender();
    }

    public void DealWithBartender()
    {
        TakeOverBartendering();
    }

    public void TakeOverBartendering()
    {
        dialogueButton.SetActive(true);
        GoToSegment(0);
    }

    public void FinishBartendering()
    {
        Debug.Log("Crikey! The real Bartender finished his shitey!");
    }

    public void AdvanceSegment()
    {
        if(currentSegmentIndex == sequenceSegments.Count-1)
        {
            FinishBartendering();
            return;
        }

        ToggleSegment(false, currentSegmentIndex);
        currentSegmentIndex++;
        ToggleSegment(true, currentSegmentIndex);
    }

    public void GoToSegment(int index)
    {
        ToggleSegment(false, currentSegmentIndex);
        currentSegmentIndex = index;
        ToggleSegment(true, currentSegmentIndex);
    }
    public void ToggleSegment(bool activeState, int index)
    {
        foreach (GameObject go in sequenceSegments[index].GetGOs())
        {
            Debug.Log("Set gameObject " + go.name + " to active state: " + activeState);
            go.SetActive(activeState);
        }
    }
}
