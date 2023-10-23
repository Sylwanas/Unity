using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoldierType : TurretType
{
    public float speed;
    public Vector2 position;
    public int damage;
    public float attackCooldown;
    public float attackRange;
    public override Unit CreateUnit(Vector2Int position, Gamestate gamestate)
    {
        return new Soldier(this, position, gamestate);
    }
}
