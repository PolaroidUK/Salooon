using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarIngredient : MonoBehaviour
{
    public SimonSaysMixingGame.DrinkIngredients thisIngredient;

    public void SelectIngredient()
    {
        FindObjectOfType<SimonSaysMixingGame>().SelectIngredient(thisIngredient);
    }
}
