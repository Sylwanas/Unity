using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace MonsterQuest
{
    public class AttackAction : IAction
    {
        private Creature _attacker;
        private Creature _target;

        private WeaponType _weaponType;

        private Ability? _ability = null;

        public AttackAction(Creature attacker,
                            Creature target,
                            WeaponType weaponType,
                            Ability? ability)
        {
            _attacker = attacker;
            _target = target;
            _weaponType = weaponType;
            _ability = ability;
        }
        public IEnumerator Execute()
        {

            yield return _attacker.presenter.FaceCreature(_target);
            yield return _attacker.presenter.Attack();

            int attackModifier = _attacker.abilityScores.strength.modifier;
            if (_weaponType.isRanged == true)
            {
                attackModifier = _attacker.abilityScores.dexterity.modifier;
            }

            if (_weaponType.isFinesse == true)
            {
                attackModifier = _attacker.abilityScores[_ability.Value].modifier;
            }

            float attackRoll = DiceHelper.Roll("1d20");
            float baseAttackRoll = attackRoll;

            if (_attacker.IsProficientWithWeapon(_weaponType))
            {
                attackRoll += _attacker.proficiencyBonus;
            }

            string capitalizeAttacker = _attacker.displayName.ToUpperFirst();
            string capitalizeDefender = _target.displayName.ToUpperFirst();

            if (attackRoll + attackModifier  >= _target.armorClass) 
            {
                bool wasCriticalHit = false;
                int rollDamage = DiceHelper.Roll(_weaponType.damageRoll);
                int damage = Math.Max(0, rollDamage + attackModifier);

                if (baseAttackRoll == 20 || _target.lifeStatus != LifeStatus.Conscious)
                {
                    wasCriticalHit = true;
                    int critDamage = DiceHelper.Roll(_weaponType.damageRoll);
                    yield return _target.ReactToDamage(critDamage + damage, wasCriticalHit);
                    Console.WriteLine($"{capitalizeAttacker} rolled a {baseAttackRoll}!\nCritting {_target.displayName} for {critDamage} extra damage with their {_weaponType.displayName} for {critDamage+damage} total damage!");
                    Console.WriteLine($"{capitalizeDefender} has {_target.hitPoints} remaining.");
                }
                else
                {
                    yield return _target.ReactToDamage(damage, wasCriticalHit);
                    Console.WriteLine($"{capitalizeAttacker} rolled a {attackRoll} and hit the {_target.displayName} with their {_weaponType.displayName} for {damage} damage!");
                    Console.WriteLine($"{capitalizeDefender} has {_target.hitPoints} HP remaining.");
                }
            }
            else 
            {
                int randomMissResponse = UnityEngine.Random.Range(0, 2);

                switch (randomMissResponse)
                {
                    case 0:
                        Console.WriteLine($"{capitalizeAttacker} rolled a {attackRoll} and missed {_target.displayName}.");
                        break;
                    case 1:
                        Console.WriteLine($"{capitalizeDefender} parries {_attacker.displayName}'s {_weaponType.displayName} as they only roll a feeble {attackRoll}.");
                        break;
                }
            }
        }
    }
}
