using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MonsterQuest
{
    public class GameManager : MonoBehaviour
    {
        private CombatManager combatManager;

        private void Awake()
        {
            combatManager = transform.Find("Combat").GetComponent<CombatManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            //Variables
            var characters = new List<string> { "Athos", "Porthos", "Aramis", "d'Artagnan" };

            //Flavor
            Console.WriteLine($"Companions {StringHelper.JoinWithAnd(characters, true)} walk on the road to Paris.");

            //Calling
            combatManager.Simulate(characters, "orc", DiceHelper.Roll("2d8+6"), 10);
            combatManager.Simulate(characters, "azer", DiceHelper.Roll("6d8+12"), 18);
            combatManager.Simulate(characters, "troll", DiceHelper.Roll("8d10+40"), 16);

            //Characters Alive
            if (characters.Count == 1)
            {
                Console.WriteLine($"Only {characters[0]} has survived.");
            }
            if (characters.Count > 1)
            {
                Console.WriteLine($"{ StringHelper.JoinWithAnd(characters, true)} have survived!");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
