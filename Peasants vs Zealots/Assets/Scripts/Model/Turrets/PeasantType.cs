using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class PeasantType : TurretType
{
    public float goldCooldown;
    public int goldAmount;

    public override Unit CreateUnit(Vector2Int position, GameManager gameManager)
    {
        return new Peasant(this, position, gameManager);
    }
}
