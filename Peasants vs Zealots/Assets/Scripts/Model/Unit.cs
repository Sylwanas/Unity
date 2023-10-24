using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Unit : IDamageAble
{
    [SerializeReference] protected Gamestate gameState;
    [SerializeReference] protected GameManager gameManager;
    [field: SerializeReference] public UnitType type { get; private set; }
    [field: SerializeField] public int health { get; private set; }
    [field: SerializeField] public Vector2 position { get; protected set; }
    public Vector2Int tilePosition => new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));

    protected Unit(UnitType type, Vector2 position, GameManager gameManager)
    {
        this.type = type;
        this.position = position;
        health = type.maxHealth;
        this.gameState = gameManager.gameState;
        this.gameManager = gameManager;
    }

    public void ReactToDamage(int damageAmount)
    {
        health = Math.Max(0, health - damageAmount);
    }

    public abstract void Update();
}
