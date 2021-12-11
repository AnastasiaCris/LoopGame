using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class NerdNavMesh : MonoBehaviour
{
    /// <summary>
    /// target0: player;
    /// target1: backpack;
    /// 
    /// </summary>
    public Transform[] target;
    NavMeshAgent agent;

    /// <summary>
    /// 0: on player reached;
    /// 1: on backpack reached;
    /// 2: on player reached(after backpack);
    /// </summary>
    public UnityEvent[] onDestinationReached;

    public bool followingHome;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = true;
        target[0] = GameObject.FindWithTag("Player").transform;
        target[0].position = new Vector2(target[0].position.x+20, target[0].position.y);
    }

    //if the MC proved to be knowledgable the "nerd" would go up to talk to him being impressed 
    public void TalkToMC()
    {
        if (GameObject.FindObjectOfType<TeacherNavMesh>().hasHw && GameObject.FindObjectOfType<TeacherNavMesh>().answerCorrect)
        {
            print("nerd incoming");
            StartCoroutine("WalkingToMc");
        }
    }

    IEnumerator WalkingToMc()
    {
        agent.isStopped = false;
        agent.SetDestination(target[0].position);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, target[0].position) <= 2);
        onDestinationReached[0].Invoke();
        //agent.isStopped = true;
    }

    public void GetBackpack()
    {
        print("getting backpack");
        StartCoroutine("WalkToBackpack");
    }

    IEnumerator WalkToBackpack()
    {
        agent.isStopped = false;
        agent.SetDestination(target[1].position);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, target[1].position) <= 1);
        onDestinationReached[1].Invoke();
        StartCoroutine("setDest");
        onDestinationReached[2].Invoke();
        followingHome = true;
        yield return null;
    }

    IEnumerator setDest()
    {
        agent.SetDestination(target[3].position);
        yield return new WaitForSeconds(0.1f);
    }
}
