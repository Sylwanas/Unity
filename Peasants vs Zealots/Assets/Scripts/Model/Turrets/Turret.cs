using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Turret : Unit
{
    protected Turret(UnitType type, Vector2 position, Gamestate gameState) : base(type, position, gameState)
    {
    }
}
