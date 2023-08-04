using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterQuest
{
    public class CombatManager : MonoBehaviour
    {
        public IEnumerator Simulate(GameState gameState)
        {

            if (gameState.party.characterCount == 0)
            {
                yield break;
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
                    yield return character.presenter.Attack();
                    yield return gameState.combat.monster.ReactToDamage(roll2D6);

                    if (gameState.combat.monster.hitPoints == 0)
                    {
                        Console.WriteLine($" The {gameState.combat.monster.displayName} has {gameState.combat.monster.hitPoints} HP remaining.");
                        Console.WriteLine("\nTriomphe! The companions continue to Paris! Tous pour un, un pour tous!\n");
                        yield break;
                    }
                    Console.WriteLine($" The {gameState.combat.monster.displayName} has {gameState.combat.monster.hitPoints} HP remaining.");
                }

                //DC Save
                Character[] characters = gameState.party.characters.ToArray();
                int roll1D20 = DiceHelper.Roll("1d20+3");
                int randomCharacterIndex = Random.Range(0, characters.Length);
                Character randomCharacter = characters[randomCharacterIndex];

                yield return gameState.combat.monster.presenter.Attack();
                if (roll1D20 < gameState.combat.monster.savingThrowDC)
                {
                    Console.WriteLine($"{randomCharacter} rolls a {roll1D20} and is turnt to mush!");
                    yield return randomCharacter.ReactToDamage(10);
                    gameState.party.RemoveCharacter(randomCharacter);
                }
                else
                {
                    Console.WriteLine($"{randomCharacter} rolls a {roll1D20} and is not turnt to mush!");
                }

                //Everyone Dead Check
                if (gameState.party.characterCount == 0)
                {
                    Console.WriteLine("All of the companions are dead! Merde!");
                    yield break;
                }
            }
        }
    }
}
