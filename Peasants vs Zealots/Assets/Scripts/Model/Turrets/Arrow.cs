using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Arrow : Turret, IRangeProvider
{
    [field: SerializeReference] public ArrowType arrowType { get; set; }
    public Arrow(ArrowType arrowType,
            Vector2Int position,
            GameManager gameManager) :
    base(arrowType,
        position,
        gameManager)
    {
        this.arrowType = arrowType;
    }

    public float attackRange => arrowType.attackRange;

    public void Attack(IDamageAble target)
    {
        target.ReactToDamage(arrowType.damage);
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

        if (position.x <= 13 && !inContact)
        {
            position.x += arrowType.speed * Time.deltaTime;
            this.position = position;
        }
    }
    public override void Update()
    {
        MoveForward();
    }
}
