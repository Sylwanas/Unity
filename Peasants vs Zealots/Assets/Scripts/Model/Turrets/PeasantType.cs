using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class PeasantType : TurretType
{
    public float goldCooldown;
    public int goldAmount;

    public override Turret CreateTurret(Vector2Int position, Gamestate gamestate)
    {
        return new Peasant(this, position, gamestate);
    }
}
