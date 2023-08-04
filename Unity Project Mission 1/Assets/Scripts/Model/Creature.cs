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

        public Creature(string displayName, Sprite bodySprite, int hitPointsMaximum, SizeCategory sizeCategory)
        {
            this.displayName = displayName;
            this.bodySprite = bodySprite;
            this.hitPointsMaximum = hitPointsMaximum;
            this.sizeCategory = sizeCategory;

            hitPoints = hitPointsMaximum;
        }

        public void InitializePresenter(CreaturePresenter presenter) 
        {
            this.presenter = presenter;  
        }

        public void ReactToDamage(int damageAmount)
        {
            hitPoints = Math.Max(0, hitPoints - damageAmount);
        }

        public override string ToString() 
        { 
            return displayName;
        }
    }
}
