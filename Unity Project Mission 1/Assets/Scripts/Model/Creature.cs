using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Creature
    {
        public CreaturePresenter presenter { get; private set; }
        public string displayName { get; protected set; }
        public Sprite bodySprite { get; protected set; }
        public int hitPointsMaximum { get; protected set; }
        public SizeCategory sizeCategory { get; protected set; }
        public int hitPoints { get; protected set; }

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
        }

        public void InitializePresenter(CreaturePresenter presenter) 
        {
            this.presenter = presenter;  
        }

        public IEnumerator ReactToDamage(int damageAmount)
        {
            hitPoints = Math.Max(0, hitPoints - damageAmount);
            if (hitPoints == 0) 
            { 
                yield return presenter.Die();
            }
            else
            {
                yield return presenter.TakeDamage();
            }
        }

        public override string ToString() 
        { 
            return displayName;
        }
    }
}
