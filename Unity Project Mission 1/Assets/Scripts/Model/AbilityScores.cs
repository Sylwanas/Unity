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

        public AbilityScores() 
        {
            strength = new AbilityScore();
            dexterity = new AbilityScore();
            constitution = new AbilityScore();
            intelligence = new AbilityScore();
            wisdom = new AbilityScore();
            charisma = new AbilityScore();
        }

        public AbilityScore this[Ability ability]
        {
            get
            {
                return ability switch
                {
                    Ability.Strength => strength,
                    Ability.Dexterity => dexterity,
                    Ability.Constitution => constitution,
                    Ability.Intelligence => intelligence,
                    Ability.Wisdom => wisdom,
                    Ability.Charisma => charisma,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}
