using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> items = new List<Item>();
}
