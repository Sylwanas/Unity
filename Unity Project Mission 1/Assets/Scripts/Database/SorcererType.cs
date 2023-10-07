using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    [CreateAssetMenu]
    public class SorcererType : ClassType
    {
        public int maxSpellSlots = 1;
        public SpellType[] spellList;
    }
}
