using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EavesdropGameManager : MonoBehaviour
{
    public EavesdropPlayer player;
    public EavesdropConversation conversation;

    public float timeToWin;
    public float gameProgress;

    public bool gameIsOn;
    public bool IsPlaying { get { return gameIsOn && Application.isPlaying; } }

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<EavesdropPlayer>();
        }
        if (conversation == null)
        {
            conversation = FindObjectOfType<EavesdropConversation>();
        }

        gameIsOn = true;
        player.EavesdropLoop();
        conversation.ConversationLoop();
    }

    public void Progress()
    {
        gameProgress += Time.deltaTime;

        if (gameProgress > timeToWin)
        {
            WinGame();
        }
    }


    public bool CheckPlayerHiding()
    {
        return player.IsHiding;
    }

    public void WinGame()
    {
        gameIsOn = false;
        Debug.Log("WOOHOO");
    }

    public void FailGame()
    {
        gameIsOn = false;
        Debug.Log("Who's that creep eavesdropping on us?!?");
    }
}
