using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EavesdropPlayer : MonoBehaviour
{
    private EavesdropGameManager eavesdropManager;

    public GameObject eavesdroppingImage;
    public GameObject hidingImage;

    public bool wasHiding;

    public EavesdropState currentState;
    public enum EavesdropState
    {
        eavesdropping,
        hiding
    }
    public bool IsHiding { get { return currentState == EavesdropState.hiding; } }

    void Start()
    {
        if (eavesdropManager == null)
        {
            eavesdropManager = FindObjectOfType<EavesdropGameManager>();
        }
        SetPlayerSprite();
    }


    public async void EavesdropLoop()
    {
        while(eavesdropManager.IsPlaying)
        {
            if(InputActive())
            {
                currentState = EavesdropState.eavesdropping;
                eavesdropManager.Progress();
            }
            else
            {
                currentState = EavesdropState.hiding;
            }

            if(wasHiding != IsHiding)
            {
                SetPlayerSprite();
                wasHiding = IsHiding;
            }
            await Task.Delay(1);
        }
    }

    public void SetPlayerSprite()
    {
        eavesdroppingImage.SetActive(currentState == EavesdropState.eavesdropping);
        hidingImage.SetActive(currentState == EavesdropState.hiding);
    }

    public bool InputActive()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }
}
