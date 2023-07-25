using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class CombatManager : MonoBehaviour
    {
        public void Simulate(List<string> characters, string monsterName, int monsterHP, int savingThrowDC)
        {
            if (characters.Count == 0)
            {
                return;
            }

            //Flavor
            Console.WriteLine($"A hideous {monsterName} with {monsterHP} HP has appeared!");

            //Damage Calculations
            while (true)
            {
                foreach (var character in characters)
                {
                    int roll2D6 = DiceHelper.Roll("2d6");
                    Console.Write(character);
                    Console.Write($" deals {roll2D6} damage!");
                    monsterHP -= roll2D6;
                    if (monsterHP <= 0)
                    {
                        monsterHP = 0;
                        Console.WriteLine($" The {monsterName} has {monsterHP} HP remaining.");
                        Console.WriteLine("\nTriomphe! The companions continue to Paris! Tous pour un, un pour tous!\n");
                        return;
                    }
                    Console.WriteLine($" The {monsterName} has {monsterHP} HP remaining.");
                }

                //DC Save
                int roll1D20 = DiceHelper.Roll("1d20+3");
                int randomChar = Random.Range(0, characters.Count);

                if (roll1D20 < savingThrowDC)
                {
                    Console.WriteLine($"{characters[randomChar]} rolls a {roll1D20} and is turnt to mush!");
                    characters.Remove(characters[randomChar]);
                }
                else
                {
                    Console.WriteLine($"{characters[randomChar]} rolls a {roll1D20} and is not turnt to mush!");
                }

                //Everyone Dead Check
                if (characters.Count == 0)
                {
                    Console.WriteLine("All of the companions are dead! Merde!");
                    return;
                }
            }
        }
    }
}
