using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Interactable
{
    bool sitting;
    [SerializeField] DialogueTrigger dialogueTrigger;

    public override void Start()
    {
        base.Start();
        sitting = false;

    }
    void Update()
    {
        if (inTrigger && Input.GetKeyDown(Hotkeys.interact))
        {
            sitting = !sitting;
        }

        if (sitting)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f);
            if (!dialogueTrigger.startedTalking)
            {
                dialogueTrigger.TriggerDialogue();
            }
        }
    }
}
