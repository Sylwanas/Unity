using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Zealot : Unit
{
    [field: SerializeReference] public ZealotType zealotType { get; private set; }
    [field: SerializeField] public float attackCountdown { get; private set; }

    public Zealot(ZealotType zealotType,
                Vector2Int position,
                Gamestate gameState) : base(zealotType, position, gameState)
    {
        this.zealotType = zealotType;
        attackCountdown = zealotType.attackCooldown;
    }

    public void MoveZealot()
    {
        Vector2 position = this.position;

        if (position.x >= -0.5)
        {
            position.x -= zealotType.speed * Time.deltaTime;
            this.position = position;
        }
    }

    public void attackCooldown(Unit unit, int damage)
    {
        attackCountdown -= Time.deltaTime;

        if (attackCountdown < 0)
        {
            unit.ReactToDamage(damage);
            attackCountdown = zealotType.attackCooldown;
            MoveBack();
        }
    }

    public void MoveBack()
    {
        Vector2 position = this.position;
        position.x += zealotType.attackCooldown;
        this.position = position;
    }

    public override void Update()
    {
        MoveZealot();
        attackCountdown -= Time.deltaTime;
    }
}
