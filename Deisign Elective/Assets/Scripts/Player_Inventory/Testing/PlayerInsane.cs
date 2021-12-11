using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInsane : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;

    void Start()
    {
        target = GameObject.Find("knife").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(target.position);
        agent.isStopped = true;
    }

    void Update()
    {
        for (int i = 0; i < Hotkeys.movement.Length; i++)
        {
            if (AnyKeyPressed())
            {
                 agent.SetDestination(target.position);

                if (agent.isStopped)
                    agent.isStopped = false;
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }

    private bool AnyKeyPressed()
    {
        bool isPressed = false;

        foreach (KeyCode key in Hotkeys.movement)
        {
            if (Input.GetKey(key))
            {
                isPressed = true;
                break;
            }
        }

        return isPressed;
    }
}
