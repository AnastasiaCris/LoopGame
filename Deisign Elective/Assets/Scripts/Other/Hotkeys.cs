using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Hotkeys
{
    public static KeyCode inventory = KeyCode.I;
    public static KeyCode interact = KeyCode.E;
    public static KeyCode[] movement = new KeyCode[]
    {
        KeyCode.W,
        KeyCode.A,
        KeyCode.D,
        KeyCode.S,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow
    };

}
