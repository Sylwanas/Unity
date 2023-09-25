using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace MonsterQuest
{
    public class Monster : Creature
    {
        static readonly bool[] _deathSavingThrows = new bool[0];
        public override IEnumerable<bool> deathSavingThrows => _deathSavingThrows;
        public override int armorClass => type.armorClass;
        public override AbilityScores abilityScores => type.abilityScores;
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
            Character attackedCharacter = characters.Random();

            WeaponType randomWeapon = type.weaponTypes.Random();

            if (abilityScores.intelligence > 7)
            {
                attackedCharacter = characters.OrderBy(character => character.hitPoints).First();
            }

            return new AttackAction(this, attackedCharacter, randomWeapon, null);
        }
    }
}
