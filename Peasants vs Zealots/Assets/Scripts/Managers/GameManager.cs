using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field: SerializeField] public Gamestate gamestate { get; private set; }
    [SerializeField] private GameboardPresenter _gameboardPresenter;

    // Start is called before the first frame update
    void Start()
    {
        gamestate = new Gamestate();

        GameboardPresenter gameboardPresenter = transform.GetComponentInChildren<GameboardPresenter>();
        gameboardPresenter.Initialize(gamestate.gameboard);
    }

    public void CreatePeasant(PeasantType peasantType, Vector2Int position)
    {
        Turret newTurret = new Peasant(peasantType, position, gamestate);
        gamestate.gameboard.AddTurret(newTurret);
        _gameboardPresenter.InitializeTurret(newTurret);
    }

    // Update is called once per frame
    void Update()
    {
        gamestate.Update();
    }
}
