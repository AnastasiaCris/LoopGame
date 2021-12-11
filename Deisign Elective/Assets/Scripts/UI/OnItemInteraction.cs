using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static InventorySystem;

public class OnItemInteraction : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Canvas canvas;

    [HideInInspector] public InventorySystem inventorySyst;
    public MyItems itemName;
    public Item item;

    private bool state = false;
    private Animator anim;

    private HorizontalLayoutGroup HLayoutGroup;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Image[] images;

    [SerializeField]private GameObject itemNameText;

    void Start()
    {
        itemNameText.SetActive(false);

        HLayoutGroup = transform.parent.GetComponent<HorizontalLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        images = gameObject.GetComponentsInChildren<Image>();

        inventorySyst = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
        anim = GetComponent<Animator>();

        for (int i = 0; i < inventorySyst.inventory.Count; i++)
        {
            if (name == inventorySyst.inventory[i].itemID.ToString())
            {
                itemName = inventorySyst.inventory[i].itemID;
            }
        }

        foreach (Item itemm in inventorySyst.inventory)
        {
            if (itemm.itemID == itemName)
            {
                item = itemm;
                break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        state = !state;
        anim.SetBool("Show", state);

        itemNameText.SetActive(!state);

    }

    public void DropItem()
    {
        inventorySyst.RemoveItem(item, true);
    }

    public void UseItem()
    {
        //check if player is insane (for kys)
    }

    //-- UI EVENTS --//

    public void OnBeginDrag(PointerEventData eventData)
    {
        for (int i = 0; i < inventorySyst.inventory.Count; i++)
        {
            if (name == inventorySyst.inventory[i].itemID.ToString())
            {
                itemName = inventorySyst.inventory[i].itemID;
            }
        }

        HLayoutGroup.enabled = false;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].maskable = true;
        }

        HLayoutGroup.enabled = true;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    //when dragging the item 
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    //when dropping the item on another item
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            print("dropped");

            //getting the data of the item that has been dropped on this item
            OnItemInteraction draggedItemScript = eventData.pointerDrag.GetComponent<OnItemInteraction>();
            MyItems draggedItemName = draggedItemScript.itemName;


            foreach (Item item in draggedItemScript.inventorySyst.inventory)
            {
                if (item.itemID == draggedItemName)
                {
                    Item draggedItem = item;

                    //print(draggedItem.itemID.ToString());
                    //print(this.item.itemID.ToString());

                    if(inventorySyst.CheckItemsCombination(draggedItem, this.item))
                    {
                        if (draggedItem.removable)
                        {
                            HLayoutGroup.enabled = true;
                            Destroy(draggedItem.itemObject);
                            inventorySyst.RemoveItem(draggedItem, false);
                        }
                        if (this.item.removable)
                        {
                            HLayoutGroup.enabled = true;
                            Destroy(this.item.itemObject);
                            inventorySyst.RemoveItem(this.item, false);
                        }
                    }
                    else
                    {
                        print("not combinable");
                    }
                    break;
                }
            }

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //show name
        itemNameText.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;
        if(!state)
        itemNameText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //hide name
        itemNameText.SetActive(false);
    }
}
