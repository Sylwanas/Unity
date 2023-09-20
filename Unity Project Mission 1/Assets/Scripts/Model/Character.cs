using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace MonsterQuest
{
    public class Character : Creature
    {
        List<bool> _deathSavingThrows = new List<bool>();
        public WeaponType weaponType { get; }
        public ArmorType armorType { get; }
        public override IEnumerable<bool> deathSavingThrows => _deathSavingThrows;
        public override int armorClass => armorType.armorClass;

        private AbilityScores _abilityScores;
        public override AbilityScores abilityScores => _abilityScores;
        public Character(string displayName,
                       Sprite bodySprite,
                       int hitPointsMaximum,
                       SizeCategory sizeCategory,
                       WeaponType weaponType,
                       ArmorType armorType) :
            base(displayName,
                 bodySprite,
                 sizeCategory)
        {
            this.hitPointsMaximum = hitPointsMaximum;
            this.weaponType = weaponType;
            this.armorType = armorType;
            RollAbilityScores();
            Initialize();
        }

        public override IAction TakeTurn(GameState gameState)
        {
            if (lifeStatus == LifeStatus.Conscious)
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

        public IEnumerator Heal(int amount)
        {
            hitPoints = Math.Min(hitPointsMaximum, hitPoints + amount);

            if (lifeStatus != LifeStatus.Conscious)
            {
                lifeStatus = LifeStatus.Conscious;
                yield return presenter.RegainConsciousness();
                presenter.ResetDeathSavingThrows();
            }

            yield return presenter.Heal();
            
        }

        public override IEnumerator ReactToDamage(int damageAmount, bool wasCriticalHit)
        {

            if (hitPoints+hitPointsMaximum < damageAmount)
            {
                lifeStatus = LifeStatus.Dead;
                hitPoints = 0;
                yield return presenter.TakeDamage(true);
                yield return presenter.Die();
                yield break;
            }

            if (lifeStatus == LifeStatus.Conscious)
            {
                hitPoints = Math.Max(0, hitPoints - damageAmount);
                if (hitPoints == 0)
                {
                    lifeStatus = LifeStatus.UnconsciousUnstable;
                    yield return presenter.TakeDamage();
                }
                yield break;
            }

            if (lifeStatus == LifeStatus.UnconsciousStable) 
            {
                lifeStatus = LifeStatus.UnconsciousUnstable;
            }

            if (lifeStatus == LifeStatus.UnconsciousUnstable)
            {
                yield return presenter.TakeDamage();

                _deathSavingThrows.Add(false);
                yield return presenter.PerformDeathSavingThrow(false);

                if (wasCriticalHit && deathSavingThrowFailures < 3)
                {
                    _deathSavingThrows.Add(false);
                    yield return presenter.PerformDeathSavingThrow(false);
                }
            }

            if (deathSavingThrowFailures == 3) 
            {
                lifeStatus = LifeStatus.Dead;
                yield return presenter.Die();
            }
        }
        public IEnumerator HandleUnconciousState()
        {
            if (lifeStatus == LifeStatus.UnconsciousStable)
            {
                yield break;
            }

            int rollResult = DiceHelper.Roll("1d20");
            if (rollResult <= 10)
            {
                _deathSavingThrows.Add(false);
                yield return presenter.PerformDeathSavingThrow(false, rollResult);

                if (rollResult == 1 && deathSavingThrowFailures < 3)
                {
                    _deathSavingThrows.Add(false);
                    yield return presenter.PerformDeathSavingThrow(false);
                }

                if (deathSavingThrowFailures == 3)
                {
                    lifeStatus = LifeStatus.Dead;
                    yield return presenter.Die();
                }
            }
            else
            {
                _deathSavingThrows.Add(true);
                yield return presenter.PerformDeathSavingThrow(true, rollResult);

                if (deathSavingThrowSuccesses == 3 && rollResult != 20)
                {
                    lifeStatus = LifeStatus.UnconsciousStable;
                    presenter.ResetDeathSavingThrows();
                }

                if (rollResult == 20)
                {
                    yield return Heal(1);
                }
            }
        }

        private void RollAbilityScores()
        {
            _abilityScores = new AbilityScores();
            Console.WriteLine($"{displayName}'s ability scores:");

            for (int i = 1; i <= 6; i++)
            {
                List<int> rolls = new();

                for (int j = 0; j < 4; j++)
                {
                    rolls.Add(DiceHelper.Roll("1d6"));
                }

                rolls.Sort();
                rolls.RemoveAt(0);
                int score = rolls.Sum();

                Ability ability = (Ability)i;
                _abilityScores[ability].score = score;
                Console.WriteLine($"{ability}: {score}");
            }
        }
    }
}

