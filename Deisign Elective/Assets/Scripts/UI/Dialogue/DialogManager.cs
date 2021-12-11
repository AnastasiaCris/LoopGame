using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DialogueText;

    public float letterSpeed = 0.01f;
    public Color fontColor;
    public Animator anim;

    public Queue<string> sentences;

    private UnityEvent onConversationEnd;
    private UnityEvent onConversationEnded2ndDay;

    void Start()
    {
        fontColor.a = 100;
        fontColor = Color.white;
        sentences = new Queue<string>();
    }

    
    public void StartDialogue (Dialogue dialogue)
    {
        anim.SetBool("isOpen", true);
       //Debug.Log("Starting Conversation with " + dialogue.name);
       if(NameText != null) { NameText.text = dialogue.name.ToString(); }
        
        onConversationEnd = dialogue.onDialogueEnd;
        onConversationEnded2ndDay = dialogue.onDialogueEnded2ndDay;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //Debug.Log("NextSentence");
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        DialogueText.text = sentence;
        DialogueText.color = fontColor;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(letterSpeed);
        }
    }

    public void EndDialogue()
    {
        anim.SetBool("isOpen", false);
        onConversationEnd.Invoke();
        if (DayCounter.day >= 2)//if it's more than the first day invoke this
        {
            onConversationEnded2ndDay.Invoke();

        }
    }

}
