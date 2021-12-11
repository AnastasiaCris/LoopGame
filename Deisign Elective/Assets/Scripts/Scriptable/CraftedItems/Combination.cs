using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCombination", menuName = "Combine")]
public class Combination : ScriptableObject
{
    [Tooltip("The two items to combine")]
    public Item[] itemsUsed = new Item[2];
    [Tooltip("The result of the combination")]
    public Item outcome;
}
