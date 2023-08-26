using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretType : ScriptableObject
{
    public int maxHealth;
    public int goldCost;
    public Sprite turretSprite;

    public abstract Turret CreateTurret(Vector2Int position, Gamestate gamestate);
}
