using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class BeUnconsciousAction : IAction
    {
        private Character _character;
        public BeUnconsciousAction(Character character)
        {
            this._character = character;
        }

        public IEnumerator Execute()
        {
            yield return _character.HandleUnconciousState();
        }
    }
}
