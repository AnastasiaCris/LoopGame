using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    BoxCollider2D boxCollider;
    public bool doorEnabled;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        if (Scenes.previousScene == 0)//if previous scene was startscene door is enabled
        {
            doorEnabled = true;
            boxCollider.enabled = true;
        }
        if (Scenes.previousScene == 1)//if previous scene was classroom door is disabled
        {
            doorEnabled = false;
            boxCollider.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        boxCollider.enabled = doorEnabled;
    }
}
