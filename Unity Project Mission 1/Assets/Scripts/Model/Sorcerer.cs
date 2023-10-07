using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Sorcerer : Character
    {
        public int spellSlots {  get; private set; }
        private SorcererType _sorcererType;
        public SpellType magicMissile = Database.GetSpellType("Magic Missile");

        public Sorcerer(string displayName, Sprite bodySprite, SizeCategory sizeCategory, WeaponType weaponType, ArmorType armorType, SorcererType sorcererType) : base(displayName, bodySprite, sizeCategory, weaponType, armorType, sorcererType)
        {
            _sorcererType = sorcererType;
            spellSlots = sorcererType.maxSpellSlots;
        }

        public override IEnumerator ShortRest()
        {
            yield return base.ShortRest();
            spellSlots = _sorcererType.maxSpellSlots;
            Console.WriteLine($"{displayName} has restored their spellslots to their maximum total of {spellSlots}");
        }

        public override IAction TakeTurn(GameState gameState)
        {
            if (spellSlots > 0 && lifeStatus == LifeStatus.Conscious) 
            {
                spellSlots--;
                return new SpellAction(this, gameState.combat.monster, magicMissile);
            }
            else if (lifeStatus == LifeStatus.Conscious)
            {
                if (weaponType.isFinesse && _abilityScores.strength < _abilityScores.dexterity)
                {
                    return new AttackAction(this, gameState.combat.monster, weaponType, Ability.Dexterity);
                }
                else
                    return new AttackAction(this, gameState.combat.monster, weaponType, Ability.Strength);
            }
            else
            {
                return new BeUnconsciousAction(this);
            }
        }
    }
}
