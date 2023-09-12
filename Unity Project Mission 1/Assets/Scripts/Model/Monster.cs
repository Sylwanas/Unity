using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterQuest
{
    public class Monster : Creature
    {
        static readonly bool[] _deathSavingThrows = new bool[0];
        public override IEnumerable<bool> deathSavingThrows => _deathSavingThrows;
        public override int armorClass => type.armorClass;

        public MonsterType type { get; private set; }

        public Monster(MonsterType type) :
            base(type.displayName,
                 type.bodySprite,
                 type.sizeCategory)
        {
            this.type = type;
            hitPointsMaximum = DiceHelper.Roll(type.hitPointsRoll);
            Initialize();
        }

        public override IAction TakeTurn(GameState gameState)
        {
            Character[] characters = gameState.party.aliveCharacters.ToArray();
            int randomCharacterIndex = Random.Range(0, characters.Length);
            Character randomCharacter = characters[randomCharacterIndex];

            int monsterWeaponIndex = Random.Range(0, type.weaponTypes.Length);

            return new AttackAction(this, randomCharacter, type.weaponTypes[monsterWeaponIndex]);
        }
    }
}
