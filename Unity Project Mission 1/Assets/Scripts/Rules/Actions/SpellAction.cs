using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace MonsterQuest
{
    public class SpellAction : IAction
    {
        private Sorcerer _caster;
        private Creature _target;
        private SpellType _spelltype;

        public SpellAction(Sorcerer caster,
                            Creature target,
                            SpellType spellType)
        {
            _caster = caster;
            _target = target;
            _spelltype = spellType;
        }
        public IEnumerator Execute()
        {
            yield return _caster.presenter.FaceCreature(_target);
            yield return _caster.presenter.Attack();

            string capitalizeDefender = _target.displayName.ToUpperFirst();
            string capitalizeCaster = _caster.displayName.ToUpperFirst();
            int rollDamage = DiceHelper.Roll(_spelltype.damageRoll);
            int damage = rollDamage * 3;

            yield return _target.ReactToDamage(damage, false);
            Console.WriteLine($"{capitalizeCaster} casts {_spelltype.displayName}! Automatically hitting the {capitalizeDefender} with 3 missiles dealing {rollDamage} each, for a total of {damage} damage!");
            Console.WriteLine($"{capitalizeCaster} has {_caster.spellSlots} spellslots remaining.");
        }
    }
}
