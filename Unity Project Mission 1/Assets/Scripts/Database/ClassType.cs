using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    [CreateAssetMenu]
    public class ClassType : ScriptableObject
    {
        public string displayName;
        public WeaponCategory[] weaponProficiencies;
        public int hitDie;
        public string hitDieRoll => $"1d{hitDie}";
    }
}
