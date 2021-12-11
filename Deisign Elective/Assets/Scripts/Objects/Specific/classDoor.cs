using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class classDoor : Interactable
{
    public UnityEvent[] onDoorInteraction;

    public void DoorInteract()
    {
        if (!GameObject.FindObjectOfType<NerdNavMesh>().followingHome)
        {
            onDoorInteraction[0].Invoke();
        }
        else
        {
            onDoorInteraction[1].Invoke();
        }
    }
}
