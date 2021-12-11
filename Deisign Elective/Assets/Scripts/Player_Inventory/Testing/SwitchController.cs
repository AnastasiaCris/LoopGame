using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwitchController : MonoBehaviour
{
    PlayerMovement pm;
    PlayerInsane pi;
    NavMeshAgent agent;

    public bool normal = true;
    public bool insane = false;
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        pi = GetComponent<PlayerInsane>();
        agent = GetComponent<NavMeshAgent>();

        pm.enabled = normal;
        pi.enabled = insane;
        agent.enabled = insane;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchMovement();
        }
    }

    void SwitchMovement()
    {
        normal = !normal;
        insane = !insane;

        pm.enabled = normal;
        pi.enabled = insane;
        agent.enabled = insane;
    }
}
