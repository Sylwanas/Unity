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

            //Intiative
            List<Creature> Initiative = new List<Creature>();
            foreach (Creature creature in gameState.party.characters)
            {
                Initiative.Add(creature);
            }
            Initiative.Add(gameState.combat.monster);
            Initiative.Shuffle();

            //Actual Running
            while (true)
            {
                IAction action;

                foreach (Creature creature in Initiative)
                {
                    if (creature.lifeStatus == LifeStatus.Dead)
                    {
                        continue;
                    }

                    action = creature.TakeTurn(gameState);
                    yield return action.Execute();

                    if (gameState.combat.monster.lifeStatus == LifeStatus.Dead)
                    {
                        Console.WriteLine("\nTriomphe! The companions continue to Paris! Tous pour un, un pour tous!\n");
                        yield break;
                    }
                    else if (gameState.party.aliveCharacterCount == 0)
                    {
                        Console.WriteLine("All of the companions are dead! Merde!");
                        yield break;
                    }
                }
            }
        }
    }
}
