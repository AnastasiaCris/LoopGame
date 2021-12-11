using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class TeacherNavMesh : MonoBehaviour
{
    public Transform[] target;
    NavMeshAgent agent;
    [SerializeField] Item homework;
    [SerializeField] Item crumbledHw;

    [SerializeField]UnityEvent onPlayerReachedHomeworkYes;
    [SerializeField] UnityEvent onPlayerReachedHomeworkCrumbledYes;

    [SerializeField] UnityEvent onPlayerReachedHomeworkNo;
    [SerializeField]UnityEvent onInitialPlaceReached;

    public bool checkHw;
    public bool hasHw;
    public bool answerCorrect;
    // Start is called before the first frame update
    void Start()
    {
        checkHw = false;
        hasHw = false;
        answerCorrect = false;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target[1].position = new Vector2(target[1].position.x, target[1].position.y);

    }

    public void answeredCorrectly()
    {
        answerCorrect = true;
    }
    public IEnumerator CollectHW()
    {
        agent.SetDestination(target[0].position);

        //wait until you have reached the destination to change it
        yield return new WaitUntil(() => Vector3.Distance(transform.position, target[0].position) <= 1);
        print("path 1 reached");
        agent.SetDestination(target[1].position);//the player

        yield return new WaitUntil(() => Vector3.Distance(transform.position, target[1].position) <= 2);
        print("path 2 reached");
        
        //check if player has homework
        if (target[1].gameObject.GetComponent<InventorySystem>().CheckForItem(homework))
        {
            hasHw = true;

            onPlayerReachedHomeworkYes.Invoke();
            print("has homework");
            yield return null;
        }
        if (target[1].gameObject.GetComponent<InventorySystem>().CheckForItem(crumbledHw))
        {
            hasHw = false;

            onPlayerReachedHomeworkCrumbledYes.Invoke();
            print("has homework crumbled");
            yield return null;
        }
        else if (!target[1].gameObject.GetComponent<InventorySystem>().CheckForItem(homework) && !target[1].gameObject.GetComponent<InventorySystem>().CheckForItem(crumbledHw))
        {
            hasHw = false;

            onPlayerReachedHomeworkNo.Invoke();
            print("no homework");
            yield return null;
        }
    }
    public void CollectHomework()
    {
        StartCoroutine("CollectHW");

    }

    public IEnumerator InitialPos()
    {
        if (target[1].gameObject.GetComponent<InventorySystem>().CheckForItem(homework))
        {
            target[1].gameObject.GetComponent<InventorySystem>().RemoveItem(homework, false);
        }

        agent.SetDestination(target[2].position);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, target[2].position) <= 1);
        onInitialPlaceReached.Invoke();

        yield return null;

    }


    public void GoToInitialPos()
    {
        StartCoroutine("InitialPos");
    }
}
