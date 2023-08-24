using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Peasant : Turret
{
    [field: SerializeReference] private PeasantType peasantType { get; set; }
    [field: SerializeField] public float goldCountdown { get; private set; }

    public Peasant(PeasantType peasantType,
                Vector2Int position,
                Gamestate gamestate) : 
        base(peasantType,
            position,
            gamestate)
    {
        this.peasantType = peasantType;
        goldCountdown = peasantType.goldCooldown;
    }

    public override void Update()
    {
        //Countdown + call player take money + reset timer 
        goldCountdown -= Time.deltaTime;

        if (goldCountdown < 0)
        {
            gamestate.player.GiveMoney(peasantType.goldAmount);
            goldCountdown = peasantType.goldCooldown;
        }        
    }
}
