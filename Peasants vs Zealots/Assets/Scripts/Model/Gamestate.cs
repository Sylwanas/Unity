using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Gamestate
{
    [field: SerializeField] public int wave { get; private set; }
    [field: SerializeField] public int enemiesAlive { get; private set; }
    [field: SerializeField] public float waveCooldown { get; private set; }
    [field: SerializeField] public float waveCountdown { get; private set; }
    [field: SerializeReference] public Gameboard gameboard { get; private set; }
    [field: SerializeReference] public Player player { get; private set; }

    public Gamestate() 
    {
        player = new Player(100);
        gameboard = new Gameboard(13, 7);
    }

    public void Update()
    {
        gameboard.Update();
    }
}
