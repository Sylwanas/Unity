using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    [CreateAssetMenu]
    public class WeaponType : ItemType
    {
        public string damageRoll;
        public bool isRanged;
        public bool isFinesse;
        public WeaponCategory[] category;
    }
}
