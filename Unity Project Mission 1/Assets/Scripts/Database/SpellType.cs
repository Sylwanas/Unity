using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    [CreateAssetMenu]
    public class SpellType : ScriptableObject
    {
        public string displayName;
        public string damageRoll;
    }
}
