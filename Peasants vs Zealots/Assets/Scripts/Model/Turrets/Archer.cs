using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Turret
{
    [field: SerializeReference] private ArcherType archerType { get; set; }
    public Archer(ArcherType archerType,
                Vector2Int position,
                Gamestate gamestate) :
        base(archerType,
            position,
            gamestate)
    {
        this.archerType = archerType;
    }
    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
