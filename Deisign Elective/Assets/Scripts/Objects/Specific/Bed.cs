using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{

    public BoxCollider2D boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        if (Scenes.previousScene == 0)//if previous scene was startscene bed is disabled
        {
            boxCollider.enabled = false;
        }
        if (Scenes.previousScene == 1)//if previous scene was classroom bed is enabled
        {
            boxCollider.enabled = true;
        }

    }
}
