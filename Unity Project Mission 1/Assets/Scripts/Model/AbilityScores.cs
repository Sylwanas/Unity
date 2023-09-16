using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    [Serializable]
    public class AbilityScores
    {
        [field: SerializeField] public AbilityScore strength { get; private set; }
        [field: SerializeField] public AbilityScore dexterity { get; private set; }
        [field: SerializeField] public AbilityScore constitution { get; private set; }
        [field: SerializeField] public AbilityScore intelligence { get; private set; }
        [field: SerializeField] public AbilityScore wisdom { get; private set; }
        [field: SerializeField] public AbilityScore charisma { get; private set; }


        public AbilityScores(AbilityScore strength,
                             AbilityScore dexterity,
                             AbilityScore constitution,
                             AbilityScore intelligence,
                             AbilityScore wisdom,
                             AbilityScore charisma) 
        {
            this.strength = strength;
            this.dexterity = dexterity;
            this.constitution = constitution;
            this.intelligence = intelligence;
            this.wisdom = wisdom;
            this.charisma = charisma;
        }
    }
}
