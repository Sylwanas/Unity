using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Soldier : Turret, IRangeProvider
{
    [field: SerializeReference] public SoldierType soldierType { get; set; }
    [field: SerializeField] public float attackCountdown { get; private set; }

    public float attackRange => soldierType.attackRange;

    public Soldier(SoldierType soldierType,
            Vector2Int position,
            GameManager gameManager) :
    base(soldierType,
        position,
        gameManager)
    {
        this.soldierType = soldierType;
        attackCountdown = 0;
    }

    public void Attack(IDamageAble target)
    {
        if (attackCountdown <= 0)
        {
            target.ReactToDamage(soldierType.damage);
            MoveBack();
            attackCountdown = soldierType.attackCooldown;
        }
    }

    public void MoveBack()
    {
        Vector2 position = this.position;
        position.x -= 1;
        this.position = position;
    }

    public void MoveForward()
    {
        bool inContact = false;
        Vector2 position = this.position;

        foreach (Zealot zealot in gameState.gameboard.zealots)
        {
            if (gameState.gameboard.AreUnitsInContact(this, zealot))
            {
                inContact = true;
                break;
            }
        }

        if (position.x <= 12 && !inContact)
        {
            position.x += soldierType.speed * Time.deltaTime;
            this.position = position;
        }
    }
    public override void Update()
    {
        MoveForward();
        attackCountdown -= Time.deltaTime;
    }
}
