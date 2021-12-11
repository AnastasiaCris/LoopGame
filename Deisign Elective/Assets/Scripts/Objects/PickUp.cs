using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : Interactable
{
    [Tooltip("Select what item this game object is")]
    public Item item;
    [Tooltip("Check if you want the item to be picked up in the inventory")]
    public bool pickUpInventory = true;
    [Tooltip("This event will trigger by pressing yes when a question box appears")]
    public UnityEvent onYesPressedForQuestionBox;

    GameObject textBoxQuestion;
    GameObject textBoxQuestionParent;
    [HideInInspector]public List<GameObject> textBoxesQuestion = new List<GameObject>();

    public override void Start()
    {
        base.Start();
        textBoxQuestionParent = GameObject.Find("CanvasInventory");
        textBoxQuestion = textBoxQuestionParent.transform.Find("TextBoxQuestionOnInteract").gameObject;
        textBoxQuestion.SetActive(false);
    }

    private void Update()
    {
        if(inTrigger && Input.GetKeyDown(Hotkeys.interact) && textBoxesQuestion.Count == 0)
        {
            InstantiateQuestion();
        }
    }

    public override void TriggerExit(Collider2D collision)
    {
        base.TriggerExit(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            if(textBoxesQuestion.Count >= 1)
            {
                Destroy(textBoxesQuestion[0]);
                textBoxesQuestion.Remove(textBoxesQuestion[0]);
            }
        }
    }

    /// <summary>
    /// Instantiates a text box question if the player should pick up the item
    /// Adds a listener for when the yes button is pressed in the instantiated question box
    /// </summary>
    private void InstantiateQuestion()
    {
        GameObject textBoxClone = Instantiate(textBoxQuestion);

        textBoxClone.transform.SetParent(textBoxQuestionParent.transform, false);
        textBoxClone.GetComponent<RectTransform>().sizeDelta = textBoxQuestion.GetComponent<RectTransform>().sizeDelta;
        textBoxClone.GetComponent<RectTransform>().localScale = textBoxQuestion.GetComponent<RectTransform>().localScale;
        textBoxClone.GetComponent<RectTransform>().localRotation = textBoxQuestion.GetComponent<RectTransform>().localRotation;
        textBoxClone.GetComponent<RectTransform>().localPosition = textBoxQuestion.GetComponent<RectTransform>().localPosition;
        textBoxClone.SetActive(true);

        textBoxClone.GetComponent<TextBoxInteract>().item = item;
        textBoxClone.GetComponent<TextBoxInteract>().objName = item.itemName;
        textBoxClone.GetComponent<TextBoxInteract>().yesB.onClick.AddListener(onYesPressedForQuestionBox.Invoke);

        textBoxesQuestion.Add(textBoxClone);
    }

    /// <summary>
    /// Destroys the instantiated textBox and removes from list
    /// </summary>
    public void DestroyTextBox()
    {
        if (textBoxesQuestion.Count >= 1)
        {
            Destroy(textBoxesQuestion[0]);
            textBoxesQuestion.Remove(textBoxesQuestion[0]);
        }
    }

    /// <summary>
    /// Picks up the item and adds it to the inventory
    /// But if the item shouldn't go in the inventory it will just be inactive
    /// </summary>
    public void PickingUp()
    {
        if (pickUpInventory)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>().AddItem(gameObject, item);
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
