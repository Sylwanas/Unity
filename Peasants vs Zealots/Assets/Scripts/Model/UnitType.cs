using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitType : ScriptableObject
{
    public int maxHealth;
    public Sprite sprite;

    public abstract Unit CreateUnit(Vector2Int position, GameManager gameManager);
}
