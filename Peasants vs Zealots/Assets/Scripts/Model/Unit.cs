using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Unit
{
    [SerializeReference] protected Gamestate gameState;
    [field: SerializeReference] public UnitType type { get; private set; }
    [field: SerializeField] public int health { get; private set; }
    [field: SerializeField] public Vector2 position { get; protected set; }

    protected Unit(UnitType type, Vector2 position, Gamestate gameState)
    {
        this.type = type;
        this.position = position;
        health = type.maxHealth;
        this.gameState = gameState;
    }

    public void ReactToDamage(int damageAmount)
    {
        health = Math.Max(0, health - damageAmount);
    }

    public abstract void Update();
}
