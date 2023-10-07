using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;

namespace MonsterQuest
{
    public class GameManager : MonoBehaviour
    {
        private CombatPresenter _combatPresenter;

        private CombatManager _combatManager;

        private GameState _gameState;

        private Character _character;

        [SerializeField] private Sprite[] characterSprites;
        [SerializeField] private MonsterType[] monsterTypes;

        private void Awake()
        {
            _combatPresenter = transform.Find("Combat").GetComponent<CombatPresenter>();
            _combatManager = transform.Find("Combat").GetComponent<CombatManager>();
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

            List<WeaponType> weaponTypes = new List<WeaponType>();
            foreach (ItemType itemType in Database.itemTypes)
            {
                if (itemType is WeaponType && itemType.weight > 0) 
                {
                    weaponTypes.Add(itemType as WeaponType);
                }
            }

            List<ArmorType> armorTypes = new List<ArmorType>();
            foreach (ItemType itemType in Database.itemTypes)
            {
                if (itemType is ArmorType && itemType.weight > 0)
                {
                    armorTypes.Add(itemType as ArmorType);
                }
            }

            ClassType fighter = Database.GetClassType("Fighter");
            SorcererType sorcerer = (SorcererType)Database.GetClassType("Sorcerer");

            for (int i = 0; i < characterNames.Length; i++)
            {
                int randomWeaponIndex = Random.Range(0, weaponTypes.Count);
                WeaponType randomWeapon = weaponTypes[randomWeaponIndex];

                int randomArmorIndex = Random.Range(0, armorTypes.Count);
                ArmorType randomArmor = armorTypes[randomArmorIndex];

                initialCharacters[i] = new Sorcerer(characterNames[i], characterSprites[i], SizeCategory.Medium, randomWeapon, randomArmor, sorcerer);
                Console.WriteLine($"{characterNames[i]} is brandishing a deadly {randomWeapon.name} and sturdy {randomArmor.name} armor.");
            }

            var party = new Party(initialCharacters);

            _gameState = new GameState(party);
        }

        private IEnumerator Simulate()
        {
            //Initializing Party
            yield return _combatPresenter.InitializeParty(_gameState);

            while (true)
            {
                //Flavor
                Console.WriteLine($"Companions {_gameState.party} walk on the road to Paris.");

                //Calling
                Monster monster;
                monster = new Monster(monsterTypes[0]);
                _gameState.EnterCombatWithMonster(monster);
                yield return _combatPresenter.InitializeMonster(_gameState);
                yield return _combatManager.Simulate(_gameState);

                foreach (Character character in _gameState.party.aliveCharacters)
                {
                    yield return character.GainExperiencePoints(_gameState.combat.monster.type.xpTotal / _gameState.party.aliveCharacterCount);
                    yield return character.ShortRest();
                }

                monster = new Monster(monsterTypes[2]);
                _gameState.EnterCombatWithMonster(monster);
                yield return _combatPresenter.InitializeMonster(_gameState);
                yield return _combatManager.Simulate(_gameState);

                foreach (Character character in _gameState.party.aliveCharacters)
                {
                    yield return character.GainExperiencePoints(_gameState.combat.monster.type.xpTotal / _gameState.party.aliveCharacterCount);
                }

                break;
            }

            //Characters Alive
            if (_gameState.party.aliveCharacterCount == 1)
            {
                Console.WriteLine($"Only {_gameState.party.aliveCharacters.First()} has survived.");
            }
            if (_gameState.party.aliveCharacterCount > 1)
            {
                Console.WriteLine($"{StringHelper.JoinWithAnd(_gameState.party.aliveCharacters.Select(character => character.displayName))} have survived.");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
