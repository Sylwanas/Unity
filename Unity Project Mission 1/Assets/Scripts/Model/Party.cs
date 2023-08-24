using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Party
    {
        private List<Character> _characters;

        public Party(IEnumerable<Character> initialCharacters)
        {
            _characters = new List<Character>(initialCharacters);
        }

        public IEnumerable<Character> characters => _characters;
        public int characterCount => _characters.Count;

        public void RemoveCharacter(Character character) 
        { 
            _characters.Remove(character);
        }

        public override string ToString()
        {
            return StringHelper.JoinWithAnd(_characters.Select(character => character.displayName), true);
        }
    }
}
