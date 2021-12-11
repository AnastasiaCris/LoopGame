using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DayCounter
{
    public static int day = 1;

    public static void AddDay(int daysToAdd)
    {
        day += daysToAdd;
        Debug.Log("It is day: " + day);
    }

}
