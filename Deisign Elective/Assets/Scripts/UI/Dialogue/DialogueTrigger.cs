using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue secondDayDialogue;
    [HideInInspector] public bool startedTalking;
    [Tooltip("A bool that checks if there should be a different dialogue once it's the day repeats")]
    public bool diffDialSecDay;
    public bool changeFont;
    public float starttypeSpeeed;
    public float typeSpeeed;
    public Color fontCol;

    private void Start()
    {
        starttypeSpeeed = GameObject.FindObjectOfType<DialogManager>().letterSpeed;
        fontCol.a = 100;
    }
    public void TriggerDialogue()
    {
        if (diffDialSecDay)
        {
            if (DayCounter.day == 1)
            {
                GameObject.FindObjectOfType<DialogManager>().StartDialogue(dialogue);
            }
            else if (DayCounter.day != 1)
            {
                GameObject.FindObjectOfType<DialogManager>().StartDialogue(secondDayDialogue);
            }
        }
        else
        {
            GameObject.FindObjectOfType<DialogManager>().StartDialogue(dialogue);
            if (changeFont)
            {
                GameObject.FindObjectOfType<DialogManager>().fontColor = fontCol;
                GameObject.FindObjectOfType<DialogManager>().letterSpeed = typeSpeeed;

            }
            else
            {
                GameObject.FindObjectOfType<DialogManager>().fontColor = Color.white;
                GameObject.FindObjectOfType<DialogManager>().letterSpeed = starttypeSpeeed;
            }
        }
        startedTalking = true;
    }
}
