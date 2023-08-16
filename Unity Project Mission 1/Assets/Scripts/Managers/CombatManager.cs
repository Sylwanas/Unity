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
                    int charRollDamage = DiceHelper.Roll($"{character.weaponType.damageRoll}");
                    Console.WriteLine($" {character.displayName} deals {charRollDamage} damage with their {character.weaponType.displayName}!");
                    yield return character.presenter.Attack();
                    yield return gameState.combat.monster.ReactToDamage(charRollDamage);

                    if (gameState.combat.monster.hitPoints == 0)
                    {
                        Console.WriteLine($" The {gameState.combat.monster.displayName} has {gameState.combat.monster.hitPoints} HP remaining.");
                        Console.WriteLine("\nTriomphe! The companions continue to Paris! Tous pour un, un pour tous!\n");
                        yield break;
                    }
                    Console.WriteLine($" The {gameState.combat.monster.displayName} has {gameState.combat.monster.hitPoints} HP remaining.");
                }

                //Monster Attack
                int monsterWeaponIndex = Random.Range(0, gameState.combat.monster.type.weaponTypes.Length);
                int monsterRoll = DiceHelper.Roll(gameState.combat.monster.type.weaponTypes[monsterWeaponIndex].damageRoll);

                Character[] characters = gameState.party.characters.ToArray();
                int randomCharacterIndex = Random.Range(0, characters.Length);
                Character randomCharacter = characters[randomCharacterIndex];

                Console.WriteLine($"{randomCharacter} is attacked and takes {monsterRoll} damage from the {gameState.combat.monster.displayName}s {gameState.combat.monster.type.weaponTypes[monsterWeaponIndex].displayName}!");
                yield return gameState.combat.monster.presenter.Attack();
                yield return randomCharacter.ReactToDamage(monsterRoll);
                Console.WriteLine($"{randomCharacter} has {randomCharacter.hitPoints} HP remaining.");
                if (randomCharacter.hitPoints == 0)
                {
                    gameState.party.RemoveCharacter(randomCharacter);
                    Console.WriteLine($"{randomCharacter} has perished!");
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
