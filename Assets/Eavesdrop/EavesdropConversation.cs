using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EavesdropConversation : MonoBehaviour
{
    private EavesdropGameManager eavesdropManager;

    public GameObject persone1Talking;
    public GameObject persone1Sussing;
    public GameObject persone1Checking;

    public GameObject persone2Talking;
    public GameObject persone2Sussing;
    public GameObject persone2Checking;

    public ConversationState currentState;
    public enum ConversationState
    {
        talking,
        sussing,
        checking
    }

    public float minTimeBetweenChecks;
    public float maxTimeBetweenChecks;
    public float timeToNextCheck;
    public float sussyWarningTime;
    public float checkingTime;
    public float stateTimer;

    private void Start()
    {
        if(eavesdropManager == null)
        {
            eavesdropManager = FindObjectOfType<EavesdropGameManager>();
        }
        SetConversationState(ConversationState.talking);
    }

    public async void ConversationLoop()
    {
        while (eavesdropManager.IsPlaying)
        {
            switch(currentState)
            {
                case ConversationState.talking:
                    ConversationTalk();
                    break;
                case ConversationState.sussing:
                    ConversationSus();
                    break;
                case ConversationState.checking:
                    ConversationCheck();
                    break;
            }
            await Task.Delay(1);
        }
    }

    public void ConversationTalk()
    {
        if (stateTimer > timeToNextCheck)
        {
            SetConversationState(ConversationState.sussing);
        }
        else
        {
            stateTimer += Time.deltaTime;
        }
    }

    public void ConversationSus()
    {
        if (stateTimer > sussyWarningTime)
        {
            SetConversationState(ConversationState.checking);
        }
        else
        {
            stateTimer += Time.deltaTime;
        }
    }

    public void ConversationCheck()
    {
        if (stateTimer > checkingTime)
        {
            SetConversationState(ConversationState.talking);
        }
        else
        {
            if(eavesdropManager.CheckPlayerHiding() == false)
            {
                eavesdropManager.FailGame();
            }
            stateTimer += Time.deltaTime;
        }
    }

    public void SetConversationState(ConversationState newState)
    {
        stateTimer = 0;
        currentState = newState;
        SetConversationImages();

        if(newState == ConversationState.talking)
        {
            timeToNextCheck = Random.Range(minTimeBetweenChecks, maxTimeBetweenChecks);
        }
    }

    public void SetConversationImages()
    {
        persone1Talking.SetActive(currentState == ConversationState.talking);
        //persone2Talking.SetActive(currentState == ConversationState.talking);
        persone1Sussing.SetActive(currentState == ConversationState.sussing);
        //persone2Sussing.SetActive(currentState == ConversationState.sussing);
        persone1Checking.SetActive(currentState == ConversationState.checking);
        //persone2Checking.SetActive(currentState == ConversationState.checking);
    }
}
