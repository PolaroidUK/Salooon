using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;

public class SimonSaysMixingGame : MonoBehaviour
{
    public int currentPatronIndex;
    public List<PatronWaddle> patrons;

    [Serializable]
    public enum DrinkIngredients
    {
        none,
        iceCube,
        citrus,
        cucumber,
        parasol,
        olive
    }

    public DrinkIngredients selectedIngredient;
    public List<DrinkIngredients> mixedIngredients;

    public GameObject drinkIngredientSelections;

    void Start()
    {
        currentPatronIndex = 0;
        CallNextPatron();
    }

    public void CallNextPatron()
    {
        patrons[currentPatronIndex].WalkIntoView();
        AwaitPatronOrder();
    }

    public async void AwaitPatronOrder()
    {
        while(patrons[currentPatronIndex].isWaddling)
        {
            await Task.Delay(1);
        }

        PrepareSimonSaysGame();
    }

    public void PrepareSimonSaysGame()
    {
        drinkIngredientSelections.SetActive(true);
    }

    public void SelectIngredient(DrinkIngredients ingredientType)
    {
        if (ingredientType != selectedIngredient)
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

            if (mixedIngredients.Count == patrons[currentPatronIndex].myOrder.Count)
            {
                CheckCompletedDrink();
            }
        }
        selectedIngredient = DrinkIngredients.none;
    }

    public void CheckCompletedDrink()
    {
        drinkIngredientSelections.SetActive(false);
        patrons[currentPatronIndex].RequestCompleted(mixedIngredients);

        mixedIngredients.Clear();
        selectedIngredient = DrinkIngredients.none;
        WaitForNextPatron();
    }

    public async void WaitForNextPatron()
    {
        while(patrons[currentPatronIndex].isWaddling)
        {
            await Task.Delay(1);
        }

        currentPatronIndex++;
        if (currentPatronIndex < patrons.Count)
        {
            CallNextPatron();
        }
        else
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        Debug.Log("Crikey! The real Bartender finished his shitey!");
        Debug.Log("Time to skip!");

        //Load the saloon scene
    }
}
