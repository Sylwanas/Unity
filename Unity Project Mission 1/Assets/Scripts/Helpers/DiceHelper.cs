using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace MonsterQuest
{
    public static class DiceHelper
    {
        private static int Roll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {
            int roll = 0;
            for (int a = 0; a < numberOfRolls; a++)
            {
                roll += Random.Range(1, diceSides + 1);
            }
            roll += fixedBonus;
            return roll;
        }

        public static int Roll(string diceNotation)
        {
            Match match = Regex.Match(diceNotation, @"(\d+)d(\d+)([+-]\d+)?");
            int numberOfRolls = int.Parse(match.Groups[1].Value);
            int diceSides = int.Parse(match.Groups[2].Value);
            if (match.Groups[3].Success) 
            {
                int fixedBonus = int.Parse(match.Groups[3].Value);
                return Roll(numberOfRolls, diceSides, fixedBonus);
            }
            else
            {
                return Roll(numberOfRolls, diceSides);
            }
        }
    }
}
