using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Turret
{
    [SerializeReference] protected Gamestate gamestate;
    [field: SerializeReference] public TurretType type { get; private set; }
    [field: SerializeField] public int health { get; private set; }
    [field: SerializeField] public Vector2Int position { get; private set; }

    public Turret(TurretType type, Vector2Int position, Gamestate gamestate)
    {
        this.type = type;
        this.position = position;
        health = type.maxHealth;
        this.gamestate = gamestate;
    }

    public IEnumerator ReactToDamage(int damageAmount)
    {
        health = Math.Max(0, health - damageAmount);
        if (health == 0)
        {
            yield break;
        }
    }

    public abstract void Update();
}
