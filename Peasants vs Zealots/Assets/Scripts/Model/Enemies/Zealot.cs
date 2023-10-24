using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Zealot : Unit, IRangeProvider
{
    [field: SerializeReference] public ZealotType zealotType { get; private set; }
    [field: SerializeField] public float attackCountdown { get; private set; }

    public float attackRange => zealotType.attackRange;

    public Zealot(ZealotType zealotType,
                Vector2Int position,
                GameManager gameManager) : base(zealotType, position, gameManager)
    {
        this.zealotType = zealotType;
        attackCountdown = 0;
    }

    public void MoveZealot()
    {
        bool inContact = false;
        Vector2 position = this.position;

        foreach (Turret turret in gameState.gameboard.turrets)
        {
            if (gameState.gameboard.AreUnitsInContact(this, turret))
            {
                inContact = true; 
                break;
            }
        }

        if (position.x >= -0.5 && !inContact)
        {
            position.x -= zealotType.speed * Time.deltaTime;
            this.position = position;
        }
    }

    public void Attack(IDamageAble target)
    {
        if (attackCountdown <= 0)
        {
            target.ReactToDamage(zealotType.damage);
            MoveBack();
            attackCountdown = zealotType.attackCooldown;
        }
    }

    public void MoveBack()
    {
        Vector2 position = this.position;
        position.x += 1;
        this.position = position;
    }

    public override void Update()
    {
        MoveZealot();
        attackCountdown -= Time.deltaTime;
    }
}
