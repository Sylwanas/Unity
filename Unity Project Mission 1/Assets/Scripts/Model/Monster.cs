using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Monster : Creature
    {
        public int savingThrowDC { get; private set; }

        public Monster(string displayName, Sprite bodySprite, int hitPointsMaximum, SizeCategory sizeCategory, int savingThrowDC) : base(displayName, bodySprite, hitPointsMaximum, sizeCategory)
        {
            this.savingThrowDC = savingThrowDC;
        }
    }
}
