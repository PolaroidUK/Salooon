using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    bool inventoryOpen = false;
    public Dictionary<Item, GameObject> items = new Dictionary<Item, GameObject>();
    public GameObject openedInventory;
    public GameObject closedInventory;

    public GameObject itemGroup;
    public GameObject itemIconPrefab;

    Item selectedItem;
    GameObject selectedItemObj;

    bool 

    private void Start()
    {
        CloseInventory();
    }

    public void ItemClicked(GameObject itemObj)
    {
        if (selectedItemObj)
            selectedItemObj.transform.GetChild(0).gameObject.SetActive(false);

        selectedItemObj = itemObj;
        selectedItem = itemObj.GetComponent<ItemInteractable>().item;
        selectedItemObj.transform.GetChild(0).gameObject.SetActive(true);
    }

    public Item GetSelectedItem()
    {
        return selectedItem;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedItemObj)
                selectedItemObj.transform.GetChild(0).gameObject.SetActive(false);
            selectedItemObj = null;
        }
    }

    public void RemoveItem(Item item)
    {
        Destroy(items[item]);
        items.Remove(item);
    }

    public void AddItem(Item item)
    {
        GameObject itemObj = Instantiate(itemIconPrefab, itemGroup.transform);
        itemObj.GetComponent<ItemInteractable>().item = item;
        items.Add(item, itemObj);
    }

    public void OpenInventory()
    {
        inventoryOpen = true;
        openedInventory.SetActive(true);
        closedInventory.SetActive(false);
    }

    public void CloseInventory()
    {
        inventoryOpen = false;
        openedInventory.SetActive(false);
        closedInventory.SetActive(true);
    }
}
