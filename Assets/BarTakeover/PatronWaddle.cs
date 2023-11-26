using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;

public class PatronWaddle : MonoBehaviour
{
    public List<SimonSaysMixingGame.DrinkIngredients> myOrder;
    public bool correctOrder;

    public Transform entryPoint;
    public Transform entryDestination;
    public Transform exitDestination;
    public Transform patronImage;
    public float walkSpeed;
    public float wobbleSpeed;
    public float wobbleTilt;
    public bool wobbleSide;
    public bool isWaddling;

    public List<PatronChatterBox> walkInTextBoxes;
    public GameObject orderTextbox;
    public List<PatronChatterBox> walkOutTextBoxesSuccess;
    public List<PatronChatterBox> walkOutTextBoxesFailure;

    [Serializable]
    public class PatronChatterBox
    {
        [HideInInspector]
        public string name = "Chat Box";
        public GameObject gameObject;
        public float showProgress;
        public float hideProgress;
    }


    private void Start()
    {
        transform.position = entryPoint.position;
        WalkIntoView();
    }

    public async void WalkIntoView()
    {
        gameObject.SetActive(true);

        float initialDistanceToDestination = Vector3.Distance(transform.position, entryDestination.position);
        float currentDistanceToDestination = 0;

        isWaddling = true;
        while(transform.position.x < entryDestination.position.x)
        {
            transform.position += new Vector3(walkSpeed * Time.deltaTime, 0, 0);

            patronImage.Rotate(new Vector3(0, 0, wobbleSide ? wobbleSpeed : -wobbleSpeed));
            float wobbleRotation = patronImage.rotation.eulerAngles.z;
            wobbleRotation = wobbleRotation > 180 ? wobbleRotation - 360 : wobbleRotation;

            if(wobbleSide && wobbleRotation > wobbleTilt ||
                !wobbleSide && wobbleRotation < -wobbleTilt)
            {
                wobbleSide = !wobbleSide;
            }

            currentDistanceToDestination = Vector3.Distance(transform.position, entryDestination.position);

            foreach (PatronChatterBox chatBox in walkInTextBoxes)
            {
                chatBox.gameObject.SetActive(currentDistanceToDestination < initialDistanceToDestination * chatBox.showProgress &&
                    currentDistanceToDestination > initialDistanceToDestination * chatBox.hideProgress);
            }
            
            await Task.Delay(1);
        }
        isWaddling = false;
        MakeRequest();
    }

    public void MakeRequest()
    {
        orderTextbox.SetActive(true);
    }

    public void RequestCompleted(List<SimonSaysMixingGame.DrinkIngredients> mixedIngredients)
    {
        orderTextbox.SetActive(false);
        correctOrder = true;

        foreach(SimonSaysMixingGame.DrinkIngredients ingredient in mixedIngredients)
        {
            if(myOrder.Contains(ingredient) == false)
            {
                correctOrder = false;
            }
        }

        WalkOutOfView();
    }

    public async void WalkOutOfView()
    {
        float initialDistanceToDestination = Vector3.Distance(transform.position, exitDestination.position);
        float currentDistanceToDestination = 0;

        isWaddling = true;
        while (transform.position.x < exitDestination.position.x)
        {
            transform.position += new Vector3(walkSpeed * Time.deltaTime, 0, 0);

            patronImage.Rotate(new Vector3(0, 0, wobbleSide ? wobbleSpeed : -wobbleSpeed));
            float wobbleRotation = patronImage.rotation.eulerAngles.z;
            wobbleRotation = wobbleRotation > 180 ? wobbleRotation - 360 : wobbleRotation;

            if (wobbleSide && wobbleRotation > wobbleTilt ||
                !wobbleSide && wobbleRotation < -wobbleTilt)
            {
                wobbleSide = !wobbleSide;
            }

            currentDistanceToDestination = Vector3.Distance(transform.position, exitDestination.position);

            if (correctOrder)
            {
                foreach (PatronChatterBox chatBox in walkOutTextBoxesSuccess)
                {
                    chatBox.gameObject.SetActive(currentDistanceToDestination < initialDistanceToDestination * chatBox.showProgress &&
                        currentDistanceToDestination > initialDistanceToDestination * chatBox.hideProgress);
                }
            }
            else
            {
                foreach (PatronChatterBox chatBox in walkOutTextBoxesFailure)
                {
                    chatBox.gameObject.SetActive(currentDistanceToDestination < initialDistanceToDestination * chatBox.showProgress &&
                        currentDistanceToDestination > initialDistanceToDestination * chatBox.hideProgress);
                }
            }

            await Task.Delay(1);
        }

        isWaddling = false;
        gameObject.SetActive(false);
    }
}
