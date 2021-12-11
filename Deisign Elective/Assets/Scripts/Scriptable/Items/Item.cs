using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventorySystem;

[CreateAssetMenu(fileName ="NewItem", menuName ="Item")]
public class Item : ScriptableObject
{
    public MyItems itemID;
    public string itemName;
    public GameObject itemDisplay;
    public GameObject itemObject;
    public Sprite itemSprite;
    public bool removable;
}
