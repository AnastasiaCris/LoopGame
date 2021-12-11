using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySystem: MonoBehaviour
{
    public List<Item> inventory = new List<Item>(ItemInventory.itemsInInventory);
    public List<Combination> recipes = new List<Combination>();

    [SerializeField] GameObject itemDisplayParent;
    [SerializeField] GameObject itemDisplay;

    [Tooltip("item outcome instantiator factors")]
    [SerializeField] GameObject itemOutcomeParent;
    [SerializeField] GameObject itemOutcome;

    private bool addedDay = false;
    public enum MyItems
    {
        knife,
        knob,
        unreadableLetter,
        readableLetter,
        UVLight,
        backpack,
        gameboy,
        homework,
        homeworkCrumbled,
        ruler,
        trash
    }

    private void Start()
    {
        itemOutcomeParent = GameObject.FindGameObjectWithTag("ItemStorage");
        itemOutcome = itemOutcomeParent.transform.Find("OutcomeExample").gameObject;
        itemOutcome.SetActive(false);

        inventory = ItemInventory.itemsInInventory;
        InstantiateItemsOnSceneChange();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DayCounter.AddDay(1);
        }
    }

    public void AddDays()
    {
        
        if (!addedDay)
        {
            DayCounter.AddDay(1);
            addedDay = true;
        }
    }

    /// <summary>
    /// Adds an item to the inventory
    /// </summary>
    /// <param name="addedItemID"> What item to add to inventory (MyItems) </param>
    /// <param name="itemSprite"> What sprite to be set in inventory display </param>
    /// <param name="itemObject"> What is the gameobject of item that's being added </param>
    public  void AddItem(GameObject itemObject, Item item)
    {
        GameObject itemDisplayClone = Instantiate(itemDisplay);

        itemDisplayClone.transform.SetParent(itemDisplayParent.transform, false);
        itemDisplayClone.GetComponent<RectTransform>().sizeDelta = itemDisplay.GetComponent<RectTransform>().sizeDelta;
        itemDisplayClone.GetComponent<RectTransform>().localScale = itemDisplay.GetComponent<RectTransform>().localScale;
        itemDisplayClone.GetComponent<RectTransform>().localRotation = itemDisplay.GetComponent<RectTransform>().localRotation;
        itemDisplayClone.GetComponent<RectTransform>().localPosition = itemDisplay.GetComponent<RectTransform>().localPosition;
        itemDisplayClone.SetActive(true);


        itemDisplayClone.name = item.itemID.ToString();
        itemDisplayClone.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemSprite;

        item.itemDisplay = itemDisplayClone;
        item.itemObject = itemObject;

        //inventory.Add(item);
        ItemInventory.itemsInInventory.Add(item);
    }

    /// <summary>
    /// Removes the item from inventory list and drops it
    /// </summary>
    /// <param name="itemToRemove"> What item to remove from inventory </param>
    public void RemoveItem(Item itemToRemove, bool drop)
    {
        GameObject itemDisplay = itemDisplayParent.transform.Find(itemToRemove.itemID.ToString()).gameObject;
        Destroy(itemDisplay);

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].name == itemToRemove.name)
            {
                GameObject itemObjectC = inventory[i].itemObject;
                if (drop)
                {
                    itemObjectC.transform.position = new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y);
                    itemObjectC.SetActive(true);
                }
                //inventory.Remove(inventory[i]);
                ItemInventory.itemsInInventory.Remove(inventory[i]);

                break;
            }
        }
    }

    public void RemoveAllItems()
    {
        while (ItemInventory.itemsInInventory.Count != 0)
        {
            RemoveItem(ItemInventory.itemsInInventory[0], false);
        }
    }

/*    IEnumerator RemoveEveryItem()
    {
        foreach (Item item in ItemInventory.itemsInInventory)
        {
            RemoveItem(item, false);
        }

        while (ItemInventory.itemsInInventory.Count != 0)
        {
            RemoveItem(ItemInventory.itemsInInventory[0], false);
        }

        yield return null;
    }*/
    public void InstantiateItemsOnSceneChange()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            //instantiate the gameobject
            GameObject itemGO = Instantiate(itemOutcome);

            itemGO.transform.SetParent(itemOutcomeParent.transform);
            itemGO.GetComponent<PickUp>().item = inventory[i];
            itemGO.name = inventory[i].itemID.ToString();
            itemGO.GetComponent<SpriteRenderer>().sprite = inventory[i].itemSprite;

            //instantiate the item display
            GameObject itemDisplayClone = Instantiate(itemDisplay);

            itemDisplayClone.transform.SetParent(itemDisplayParent.transform, false);
            itemDisplayClone.GetComponent<RectTransform>().sizeDelta = itemDisplay.GetComponent<RectTransform>().sizeDelta;
            itemDisplayClone.GetComponent<RectTransform>().localScale = itemDisplay.GetComponent<RectTransform>().localScale;
            itemDisplayClone.GetComponent<RectTransform>().localRotation = itemDisplay.GetComponent<RectTransform>().localRotation;
            itemDisplayClone.GetComponent<RectTransform>().localPosition = itemDisplay.GetComponent<RectTransform>().localPosition;
            itemDisplayClone.SetActive(true);

            itemDisplayClone.name = inventory[i].itemID.ToString();
            itemDisplayClone.transform.Find("ItemSprite").GetComponent<Image>().sprite = inventory[i].itemSprite;

            inventory[i].itemDisplay = itemDisplayClone;
            inventory[i].itemObject = itemGO;
        }
    }

    /// <summary>
    /// Checks if the 2 provided items can be combined into a new item
    /// </summary>
    /// <param name="item1"> Give first item to combine </param>
    /// <param name="item2"> Give second item to combine </param>
    /// <returns></returns>
    public bool CheckItemsCombination(Item item1, Item item2)
    {
        bool combinable = false;
        for (int i = 0; i < recipes.Count; i++)
        {
            if((item1 == recipes[i].itemsUsed[0] && item2 == recipes[i].itemsUsed[1]) || (item1 == recipes[i].itemsUsed[1] && item2 == recipes[i].itemsUsed[0]))
            {
                combinable = true;
                GameObject outcomeObject = Instantiate(itemOutcome);

                outcomeObject.transform.SetParent(itemOutcomeParent.transform);
                outcomeObject.GetComponent<PickUp>().item = recipes[i].outcome;
                outcomeObject.name = recipes[i].outcome.itemID.ToString();
                outcomeObject.GetComponent<SpriteRenderer>().sprite = recipes[i].outcome.itemSprite;

                AddItem(outcomeObject, recipes[i].outcome);
                print("Outcome: " + recipes[i].outcome);
            }
        }
        return combinable;
    }

    /// <summary>
    /// This function will check if the player has a specific item in his inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool CheckForItem(Item item)
    {
        bool hasItem = false;

        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i] == item)
            {
                hasItem = true;
                break;
            }
        }

        return hasItem;
    }

}
