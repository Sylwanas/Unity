using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.TextCore.Text;

namespace MonsterQuest
{
    public class GameManager : MonoBehaviour
    {
        private CombatPresenter combatPresenter;

        private CombatManager combatManager;

        private GameState gameState;

        [SerializeField] private Sprite[] characterSprites;
        [SerializeField] private Sprite[] monsterSprites;

        private void Awake()
        {
            combatPresenter = transform.Find("Combat").GetComponent<CombatPresenter>();
            combatManager = transform.Find("Combat").GetComponent<CombatManager>();
        }

        IEnumerator Start()
        {
            NewGame();
            yield return Simulate();
        }

        private void NewGame()
        {
            var characterNames = new string[] { "Athos", "Porthos", "Aramis", "d'Artagnan" };
            Character[] initialCharacters = new Character[characterNames.Length];
            for (int i = 0; i < characterNames.Length; i++)
            {
                initialCharacters[i] = new Character(characterNames[i], characterSprites[i], 10, SizeCategory.Medium);
            }

            var party = new Party(initialCharacters);

            gameState = new GameState(party);
        }

        private IEnumerator Simulate()
        {
            //Initializing Party
            combatPresenter.InitializeParty(gameState);

            //Flavor
            Console.WriteLine($"Companions {gameState.party} walk on the road to Paris.");

            //Calling
            Monster monster;

            monster = new Monster("orc", monsterSprites[0], DiceHelper.Roll("2d8+6"), SizeCategory.Medium, 10);
            gameState.EnterCombatWithMonster(monster);
            combatPresenter.InitializeMonster(gameState);
            yield return combatManager.Simulate(gameState);

            monster = new Monster("azer", monsterSprites[1], DiceHelper.Roll("6d8+12"), SizeCategory.Medium, 18);
            gameState.EnterCombatWithMonster(monster);
            combatPresenter.InitializeMonster(gameState);
            yield return combatManager.Simulate(gameState);

            monster = new Monster("troll", monsterSprites[2], DiceHelper.Roll("8d10+40"), SizeCategory.Large, 16);
            gameState.EnterCombatWithMonster(monster);
            combatPresenter.InitializeMonster(gameState);
            yield return combatManager.Simulate(gameState);

            //Characters Alive
            if (gameState.party.characterCount == 1)
            {
                Console.WriteLine($"Only {gameState.party.characters.First()} has survived.");
            }
            if (gameState.party.characterCount > 1)
            {
                Console.WriteLine($"{gameState.party} have survived!");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
