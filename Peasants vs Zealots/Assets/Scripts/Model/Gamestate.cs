using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestate
{
    public int wave { get; private set; }
    public int enemiesAlive { get; private set; }
    public float waveCooldown { get; private set; }
    public float waveCountdown { get; private set; }
    public Gameboard gameboard { get; }

    public Gamestate() 
    { 
        gameboard = new Gameboard(13, 7);
    }
}
