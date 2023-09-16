using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    [CreateAssetMenu]
    public class MonsterType : ScriptableObject
    {
        public string displayName;
        public string alignment;
        public string hitPointsRoll;
        public int armorClass;
        public Sprite bodySprite;
        public SizeCategory sizeCategory;
        public WeaponType[] weaponTypes;
        public ArmorType armorType;
        public AbilityScores abilityScores;
    }
}
