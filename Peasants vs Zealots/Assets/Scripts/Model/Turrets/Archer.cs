using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Archer : Turret
{
    [SerializeField] private GameboardPresenter myGameboardPresenter;
    [field: SerializeReference] private ArcherType archerType { get; set; }
    [field: SerializeField] public float attackCountdown { get; private set; }
    public Archer(ArcherType archerType,
                Vector2Int position,
                GameManager gameManager) :
        base(archerType,
            position,
            gameManager)
    {
        this.archerType = archerType;
        attackCountdown = 0;
    }
    public override void Update()
    {
        attackCountdown -= Time.deltaTime;

        Vector2Int v2i = Vector2Int.RoundToInt(position);

        if (attackCountdown <= 0) 
        {
            gameManager.CreateUnit(archerType.arrowType, v2i);

            attackCountdown = archerType.attackCooldown;
        }
    }
}
