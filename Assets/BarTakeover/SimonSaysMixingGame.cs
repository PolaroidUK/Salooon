using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;

public class SimonSaysMixingGame : MonoBehaviour
{
    public PatronWaddle currentPatron;
    public List<PatronWaddle> patrons;

    [Serializable]
    public enum DrinkIngredients
    {
        none,
        iceCube,
        iceCubes,
        citrus,
        kiwi,
        umbrella,
        olives,
        olive
    }

    public DrinkIngredients selectedIngredient;
    public List<DrinkIngredients> mixedIngredients;

    public GameObject dialogueButton;

    void Start()
    {
        currentPatron = patrons[0];
        currentPatron.gameObject.SetActive(true);
        currentPatron.WalkIntoView();

        AwaitPatronOrder();
    }

    public async void AwaitPatronOrder()
    {
        while(currentPatron.isWaddling)
        {
            await Task.Delay(1);
        }

        PrepareSimonSaysGame();
    }

    public void PrepareSimonSaysGame()
    {

    }

    public void SelectIngredient(DrinkIngredients ingredientType)
    {
        if (selectedIngredient != selectedIngredient)
        {
            selectedIngredient = ingredientType;
        }
        else
        {
            selectedIngredient = DrinkIngredients.none;
        }
    }

    public void AddIngredient()
    {
        if (selectedIngredient != DrinkIngredients.none &&
            mixedIngredients.Contains(selectedIngredient) == false)
        {
            mixedIngredients.Add(selectedIngredient);

            CheckCompletedDrink();
        }
    }

    public void CheckCompletedDrink()
    {
        currentPatron.RequestCompleted(mixedIngredients);
    }
}
