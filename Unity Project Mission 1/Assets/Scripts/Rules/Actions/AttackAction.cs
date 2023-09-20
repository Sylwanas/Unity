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

            int attackRoll = DiceHelper.Roll("1d20");

            if (attackRoll + attackModifier >= _target.armorClass || attackRoll == 20) 
            {
                bool wasCriticalHit = false;
                int rollDamage = DiceHelper.Roll(_weaponType.damageRoll);
                int damage = Math.Max(0, rollDamage + attackModifier);

                if (attackRoll == 20 || _target.lifeStatus != LifeStatus.Conscious)
                {
                    wasCriticalHit = true;
                    int critDamage = DiceHelper.Roll(_weaponType.damageRoll);
                    yield return _target.ReactToDamage(critDamage + damage, wasCriticalHit);
                    Console.WriteLine($"{_attacker.displayName} rolled a {attackRoll}!\nCritting {_target.displayName} for {critDamage} extra damage with their {_weaponType.displayName} for {critDamage+damage} total damage!");
                    Console.WriteLine($"{_target.displayName} has {_target.hitPoints} remaining.");
                }
                else
                {
                    yield return _target.ReactToDamage(damage, wasCriticalHit);
                    Console.WriteLine($"{_attacker.displayName} rolled a {attackRoll} and hit the {_target.displayName} with their {_weaponType.displayName} for {damage} damage!");
                    Console.WriteLine($"{_target.displayName} has {_target.hitPoints} HP remaining.");
                }
            }
            else 
            {
                int randomMissResponse = UnityEngine.Random.Range(0, 2);

                switch (randomMissResponse)
                {
                    case 0:
                        Console.WriteLine($"{_attacker.displayName} rolled a {attackRoll} and missed {_target.displayName}.");
                        break;
                    case 1:
                        Console.WriteLine($"{_target.displayName} parries {_attacker.displayName}'s {_weaponType.displayName} as they only roll a feeble {attackRoll}.");
                        break;
                }
            }
        }
    }
}
