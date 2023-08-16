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
        [SerializeField] private MonsterType[] monsterTypes;

        private void Awake()
        {
            combatPresenter = transform.Find("Combat").GetComponent<CombatPresenter>();
            combatManager = transform.Find("Combat").GetComponent<CombatManager>();
        }

        IEnumerator Start()
        {
            yield return Database.Initialize();
            NewGame();
            yield return Simulate();
        }

        private void NewGame()
        {
            var characterNames = new string[] { "Athos", "Porthos", "Aramis", "d'Artagnan" };
            Character[] initialCharacters = new Character[characterNames.Length];
            ArmorType studdedLeather = Database.GetItemType<ArmorType>("Studded Leather");

            List<WeaponType> weaponTypes = new List<WeaponType>();
            foreach (ItemType itemType in Database.itemTypes)
            {
                if (itemType is WeaponType && itemType.weight > 0) 
                {
                    weaponTypes.Add(itemType as WeaponType);
                }
            }

            for (int i = 0; i < characterNames.Length; i++)
            {
                int randomWeaponIndex = Random.Range(0, weaponTypes.Count);
                WeaponType randomWeapon = weaponTypes[randomWeaponIndex];
                initialCharacters[i] = new Character(characterNames[i], characterSprites[i], 10, SizeCategory.Medium, randomWeapon, studdedLeather);
                Console.WriteLine($"{characterNames[i]} is brandishing a deadly {randomWeapon.name} and sturdy {studdedLeather.name}");
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

            monster = new Monster(monsterTypes[0]);
            gameState.EnterCombatWithMonster(monster);
            combatPresenter.InitializeMonster(gameState);
            yield return combatManager.Simulate(gameState);

            monster = new Monster(monsterTypes[1]);
            gameState.EnterCombatWithMonster(monster);
            combatPresenter.InitializeMonster(gameState);
            yield return combatManager.Simulate(gameState);

            monster = new Monster(monsterTypes[2]);
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
