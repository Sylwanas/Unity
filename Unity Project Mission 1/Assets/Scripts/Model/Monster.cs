using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Monster : Creature
    {
        public MonsterType type { get; private set; }

        public Monster(MonsterType type) :
            base(type.displayName,
                 type.bodySprite,
                 type.sizeCategory)
        {
            this.type = type;
            hitPointsMaximum = DiceHelper.Roll(type.hitPointsRoll);
            Initialize();
        }
    }
}
