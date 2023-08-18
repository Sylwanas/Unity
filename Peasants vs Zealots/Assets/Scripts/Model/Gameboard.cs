using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboard
{
    public int width { get; } 
    public int height { get; }

    public Gameboard(int width, int height) 
    { 
        this.width = width;
        this.height = height;
    }
}
