using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArcherType : TurretType
{
    public override Unit CreateUnit(Vector2Int position, Gamestate gamestate)
    {
        return new Archer(this, position, gamestate);
    }
}
