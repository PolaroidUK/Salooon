using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

<<<<<<< Updated upstream
    bool select;
    public Item item;

=======
>>>>>>> Stashed changes
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
        select = true;
    }

    public Item GetSelectedItem()
    {
        return selectedItem;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!select)
                StartCoroutine(DeselectNextFrame());
            select = false;
        }
    }

    IEnumerator DeselectNextFrame()
    {
        yield return new WaitForEndOfFrame();
        if (selectedItemObj)
            selectedItemObj.transform.GetChild(0).gameObject.SetActive(false);
        selectedItemObj = null;
        selectedItem = null;
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
        itemObj.GetComponent<Button>().onClick.AddListener(() => ItemClicked(itemObj));
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
