using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace MonsterQuest
{
    public class AttackAction : IAction
    {
        private Creature _attacker;
        private Creature _target;
        private WeaponType _weaponType;

        public AttackAction(Creature attacker,
                            Creature target,
                            WeaponType weaponType)
        {
            this._attacker = attacker;
            this._target = target;
            this._weaponType = weaponType;
        }
        public IEnumerator Execute()
        {
            yield return _attacker.presenter.FaceCreature(_target);
            yield return _attacker.presenter.Attack();
            int attackRoll = DiceHelper.Roll("1d20");

            if (attackRoll >= _target.armorClass) 
            {
                bool wasCriticalHit = false;
                int rollDamage = DiceHelper.Roll(_weaponType.damageRoll);

                if (attackRoll == 20 || _target.lifeStatus != LifeStatus.Conscious)
                {
                    wasCriticalHit = true;
                    int critDamage = DiceHelper.Roll(_weaponType.damageRoll);
                    yield return _target.ReactToDamage(critDamage + rollDamage, wasCriticalHit);
                    Console.WriteLine($"{_attacker.displayName} rolled a {attackRoll}!\nCritting {_target.displayName} for {critDamage} extra damage with their {_weaponType.displayName} for {critDamage+rollDamage} total damage!");
                    Console.WriteLine($"{_target.displayName} has {_target.hitPoints} remaining.");
                }
                else
                {
                    yield return _target.ReactToDamage(rollDamage, wasCriticalHit);
                    Console.WriteLine($"{_attacker.displayName} rolled a {attackRoll} and hit the {_target.displayName} with their {_weaponType.displayName} for {rollDamage} damage!");
                    Console.WriteLine($"{_target.displayName} has {_target.hitPoints} HP remaining.");
                }
            }
            else 
            {
                int randomMissResponse = Random.Range(0, 2);

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
