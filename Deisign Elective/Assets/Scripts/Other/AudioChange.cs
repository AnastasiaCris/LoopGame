using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChange : MonoBehaviour
{
    AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();

        audioS.pitch -= DayCounter.day * 0.2f;

/*        switch (DayCounter.day)
        {
            case 1:
                audioS.pitch -= 0.5f;
                print("day 1 audio check");
                break;

            case 2:
                audioS.pitch -= 0.7f;
                print("day 2 audio check");
                break;

            default: print("double helo");
                break;
        }*/
    }
}
