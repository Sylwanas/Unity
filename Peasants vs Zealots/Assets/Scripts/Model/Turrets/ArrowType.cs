using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArrowType : TurretType
{
    public float speed;
    public Vector2 position;
    public int damage;
    public float attackRange;

    public override Unit CreateUnit(Vector2Int position, GameManager gameManager)
    {
        return new Arrow(this, position, gameManager);
    }
}
