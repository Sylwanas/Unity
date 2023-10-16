using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Gamestate;

[Serializable]
public class Gamestate
{
    public enum WaveState
    {
        Fight,
        Cooldown,
        gameOver
    }

    [field: SerializeField] public int wave { get; private set; }
    [field: SerializeField] public WaveState waveState { get; private set; }
    [field: SerializeField] public float waveCountdown { get; private set; }
    [field: SerializeReference] public Gameboard gameboard { get; private set; }
    [field: SerializeReference] public Player player { get; protected set; }

    public Gamestate() 
    {
        player = new Player(100);
        gameboard = new Gameboard(13, 7);

        gameboard.InitializePlayer(player);
    }

    public void WaveIncrease()
    {
        wave++;
        waveState = WaveState.Fight;
    }

    public void WaveCountdown(float waveCooldown)
    {
        if (waveCountdown <= 0)
        {
            waveCountdown = waveCooldown;
        }

        waveCountdown -= Time.deltaTime;
    }

    public void WaveEnd()
    {
        waveState = Gamestate.WaveState.Cooldown;
    }

    public void gameOver()
    {
        waveState = Gamestate.WaveState.gameOver;
    }

    public void Update()
    {
        gameboard.Update();
    }
}
