using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Character : Creature
    {
        public WeaponType weaponType { get; }
        public ArmorType armorType { get; }
        public Character(string displayName,
                       Sprite bodySprite,
                       int hitPointsMaximum,
                       SizeCategory sizeCategory,
                       WeaponType weaponType,
                       ArmorType armorType) :
            base(displayName,
                 bodySprite,
                 sizeCategory)
        {
            this.hitPointsMaximum = hitPointsMaximum;
            this.weaponType = weaponType;
            this.armorType = armorType;
            Initialize();
        }
    }
}
