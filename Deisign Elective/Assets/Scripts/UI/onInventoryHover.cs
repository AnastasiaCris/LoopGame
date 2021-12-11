using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class onInventoryHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject arrow;

    private InventorySystem inventorySys;

    void Start()
    {
        arrow.SetActive(false);
        inventorySys = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject)
        {
            if (inventorySys.inventory.Count >= 4)
            {
                arrow.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject)
        {
                arrow.SetActive(false);
        }
    }


}
