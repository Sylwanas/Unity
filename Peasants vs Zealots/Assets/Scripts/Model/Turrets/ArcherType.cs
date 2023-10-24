using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArcherType : TurretType
{
    public float attackCooldown;
    public ArrowType arrowType;
    public override Unit CreateUnit(Vector2Int position, GameManager gameManager)
    {
        return new Archer(this, position, gameManager);
    }
}
