using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBoxInteract : MonoBehaviour
{
    private TextMeshProUGUI question;
    [HideInInspector] public string objName = "";
    [HideInInspector] public Item item;
    public Button yesB;
    [SerializeField] Button noB;

    [SerializeField] PickUp[] interactables;

    void Start()
    {
        interactables = GameObject.FindObjectsOfType<PickUp>();

        question = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        question.text = "Do you want to pick up the " + objName + " ?";
        SetUpTextBox();
    }
    public void SetUpTextBox()
    {
        for (int i = 0; i < interactables.Length; i++)
        {
            if (interactables[i].item == item && interactables[i].inTrigger)
            {
                yesB.onClick.AddListener(interactables[i].PickingUp);
                noB.onClick.AddListener(interactables[i].DestroyTextBox);
            }
        }
    }
}
