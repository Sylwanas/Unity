using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private float myWaveCooldown;
    [SerializeField] private ZealotType myZealotType;

    [field: SerializeField] public Gamestate gameState { get; private set; }
    [SerializeField] private GameboardPresenter myGameboardPresenter;

    public TextMeshProUGUI gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        gameState = new Gamestate();
        
        myGameboardPresenter.Initialize(gameState.gameboard);
        gameState.gameboard.InitializePresenter(myGameboardPresenter);
    }

    public void CreateUnit(UnitType unitType, Vector2Int position)
    {
        if (unitType is TurretType turret)
        {
            if (gameState.player.currentGold >= turret.goldCost)
            {
                gameState.player.BuyTurret(turret.goldCost);

                Unit newUnit = unitType.CreateUnit(position, this);
                myGameboardPresenter.InitializeUnit(newUnit);
                gameState.gameboard.AddUnit(newUnit);
            }
        }

        else
        {
            Unit newUnit = unitType.CreateUnit(position, this);
            myGameboardPresenter.InitializeUnit(newUnit);
            gameState.gameboard.AddUnit(newUnit);
        }
    }

    public void StartWave()
    {
        gameState.WaveIncrease();

        for (int i = 0; i < gameState.wave; i++)
        {
            CreateUnit(myZealotType, new Vector2Int(13, Random.Range(0, 7)));
        }
    }

    private void HandleWaves()
    {
        if (gameState.waveState == Gamestate.WaveState.Cooldown)
        {
            gameState.WaveCountdown(myWaveCooldown);

            if (gameState.waveCountdown <= 0)
            {
                StartWave();
            }
        }
        else if (gameState.waveState == Gamestate.WaveState.Fight) 
        { 
            if (gameState.gameboard.zealotCount == 0)
            {
                gameState.WaveEnd();
            }
        }

        if (gameState.player.health == 0)
        {
            gameState.gameOver();
            gameOverText.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleWaves();
        gameState.Update();
    }
}
