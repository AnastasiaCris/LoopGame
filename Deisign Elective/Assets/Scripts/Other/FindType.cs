using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindType : MonoBehaviour
{
    public DialogueTrigger[] dialogueTriggers ;
    // Start is called before the first frame update
    void Start()
    {
        dialogueTriggers = GameObject.FindObjectsOfType<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
