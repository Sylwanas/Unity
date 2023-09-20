using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public abstract class Creature
    {
        public LifeStatus _lifeStatus;
        public CreaturePresenter presenter { get; private set; }
        public string displayName { get; protected set; }
        public Sprite bodySprite { get; protected set; }
        public int hitPointsMaximum { get; protected set; }
        public SizeCategory sizeCategory { get; protected set; }
        public int hitPoints { get; protected set; }
        public abstract IEnumerable<bool> deathSavingThrows { get; }
        public int deathSavingThrowSuccesses => deathSavingThrows.Count(deathSavingThrow => deathSavingThrow);
        public int deathSavingThrowFailures => deathSavingThrows.Count(deathSavingThrow => !deathSavingThrow);
        public abstract int armorClass { get; }
        public abstract AbilityScores abilityScores { get; }

        public LifeStatus lifeStatus 
        {
            get => _lifeStatus;
            protected set 
            {
                _lifeStatus = value;
                presenter?.UpdateStableStatus();
            }
        }

        public float spaceInFeet => SizeHelper.spaceInFeetPerSizeCategory[sizeCategory];

        public Creature(string displayName, Sprite bodySprite, SizeCategory sizeCategory)
        {
            this.displayName = displayName;
            this.bodySprite = bodySprite;
            this.sizeCategory = sizeCategory;
        }

        protected void Initialize()
        {
            hitPoints = hitPointsMaximum;
            lifeStatus = LifeStatus.Conscious;
        }

        public void InitializePresenter(CreaturePresenter presenter) 
        {
            this.presenter = presenter;  
        }

        public abstract IAction TakeTurn(GameState gameState);

        public virtual IEnumerator ReactToDamage(int damageAmount, bool wasCriticalHit)
        {
            hitPoints = Math.Max(0, hitPoints - damageAmount);
            yield return presenter.TakeDamage();

            if (hitPoints == 0) 
            { 
                yield return presenter.Die();
                lifeStatus = LifeStatus.Dead;
            }
        }

        public override string ToString() 
        { 
            return displayName;
        }
    }
}
