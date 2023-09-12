using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Party
    {
        private List<Character> _characters;
        public IEnumerable<Character> characters => _characters;
        public IEnumerable<Character> aliveCharacters => _characters.Where(character => character.lifeStatus != LifeStatus.Dead);
        public int characterCount => _characters.Count;
        public int aliveCharacterCount => aliveCharacters.Count();

        public Party(IEnumerable<Character> initialCharacters)
        {
            _characters = new List<Character>(initialCharacters);
        }

        public override string ToString()
        {
            return StringHelper.JoinWithAnd(_characters.Select(character => character.displayName), true);
        }
    }
}
