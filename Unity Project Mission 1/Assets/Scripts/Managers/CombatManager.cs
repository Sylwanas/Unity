using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterQuest
{
    public class CombatManager : MonoBehaviour
    {
        public void Simulate(GameState gameState)
        {

            if (gameState.party.characterCount == 0)
            {
                return;
            }

            //Flavor
            Console.WriteLine($"A hideous {gameState.combat.monster.displayName} with {gameState.combat.monster.hitPoints} HP has appeared!");

            //Damage Calculations
            while (true)
            {
                foreach (var character in gameState.party.characters)
                {
                    int roll2D6 = DiceHelper.Roll("2d6");
                    Console.Write(character.displayName);
                    Console.Write($" deals {roll2D6} damage!");
                    gameState.combat.monster.ReactToDamage(roll2D6);

                    if (gameState.combat.monster.hitPoints == 0)
                    {
                        Console.WriteLine($" The {gameState.combat.monster.displayName} has {gameState.combat.monster.hitPoints} HP remaining.");
                        Console.WriteLine("\nTriomphe! The companions continue to Paris! Tous pour un, un pour tous!\n");
                        return;
                    }
                    Console.WriteLine($" The {gameState.combat.monster.displayName} has {gameState.combat.monster.hitPoints} HP remaining.");
                }

                //DC Save
                Character[] characters = gameState.party.characters.ToArray();
                int roll1D20 = DiceHelper.Roll("1d20+3");
                int randomChar = Random.Range(0, characters.Length);

                if (roll1D20 < gameState.combat.monster.savingThrowDC)
                {
                    Console.WriteLine($"{characters[randomChar]} rolls a {roll1D20} and is turnt to mush!");
                    gameState.party.RemoveCharacter(characters[randomChar]);
                }
                else
                {
                    Console.WriteLine($"{characters[randomChar]} rolls a {roll1D20} and is not turnt to mush!");
                }

                //Everyone Dead Check
                if (gameState.party.characterCount == 0)
                {
                    Console.WriteLine("All of the companions are dead! Merde!");
                    return;
                }
            }
        }
    }
}
