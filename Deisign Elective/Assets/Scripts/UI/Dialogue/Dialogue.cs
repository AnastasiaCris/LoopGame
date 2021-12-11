using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public enum Names
    {
        Unknown,
        Aunt,
        Teacher,
        AlexDunphy,
        Me
    }

    public Names name;

    [TextArea(3, 10)]
    public string[] sentences;

    public UnityEvent onDialogueEnd;
    public UnityEvent onDialogueEnded2ndDay;
    public UnityEvent[] onDialogueEnded3rdDay =  new UnityEvent[1];
}
