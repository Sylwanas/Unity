using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    [field: SerializeField] public int maxHealth { get; private set; }
    [field: SerializeField] public int health { get; private set; }
    [field: SerializeField] public int currentGold { get; private set; }

    public Player(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public void GiveMoney(int goldAmount)
    {
        currentGold += goldAmount;
    }

    public void ReactToDamage(int damageAmount)
    {
        health = Math.Max(0, health - damageAmount);
    }
}
