using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class ZealotType : UnitType
{
    public float speed;
    public Vector2 position;
    public int damage;
    public float attackCooldown;

    public override Unit CreateUnit(Vector2Int position, Gamestate gamestate)
    {
        return new Zealot(this, position, gamestate);
    }
}
