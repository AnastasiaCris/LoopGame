using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    [HideInInspector] public bool inTrigger = false;

    [SerializeField] GameObject canInteractText;//when u enter trigger area it shows a text
    GameObject canvasPressE;
    [SerializeField] GameObject blinker;

    [SerializeField] UnityEvent onEPressed;
    public bool EPressed = false;

    /// <summary>
    /// Instantiate the blinker and the "Press E" text on top of the gameobject
    /// </summary>
    public virtual void Start()
    {
        blinker = Instantiate(blinker);
        blinker.transform.SetParent(transform, false);

        Vector2 posy = transform.position;
        posy.y += GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.12f;
        blinker.transform.position = posy;
        blinker.SetActive(true);

        canvasPressE = Instantiate(canInteractText);
        canvasPressE.transform.SetParent(transform, false);
        canvasPressE.GetComponent<RectTransform>().sizeDelta = canInteractText.GetComponent<RectTransform>().sizeDelta;
        canvasPressE.GetComponent<RectTransform>().localScale = canInteractText.GetComponent<RectTransform>().localScale;
        canvasPressE.GetComponent<RectTransform>().localRotation = canInteractText.GetComponent<RectTransform>().localRotation;
        canvasPressE.GetComponent<RectTransform>().localPosition = canInteractText.GetComponent<RectTransform>().localPosition;

        Vector2 pressEposy = transform.position;
        pressEposy.y += GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.3f;
        canvasPressE.transform.position = pressEposy;
        canvasPressE.SetActive(false);

    }

    private void Update()
    {
        if (inTrigger && Input.GetKeyDown(Hotkeys.interact) && !EPressed)
        {
            onEPressed.Invoke();
            EPressed = true;
        }
    }

    //Instantiate "Press E" text when entering the trigger zone and hide the blinker
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inTrigger = true;
            canvasPressE.SetActive(true);
            blinker.SetActive(false);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inTrigger = false;
            canvasPressE.SetActive(false);
            blinker.SetActive(true);
            EPressed = false;
        }
        TriggerExit(collision);
    }
    public virtual void TriggerEnter(Collider2D col)
    {

    }
    public virtual void TriggerExit(Collider2D col)
    {

    }
}
