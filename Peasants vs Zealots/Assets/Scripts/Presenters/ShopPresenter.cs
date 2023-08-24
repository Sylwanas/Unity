using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPresenter : MonoBehaviour
{
    [SerializeField] private PeasantType _peasantType;

    private Gamestate _gamestate;

    [SerializeField] private GameboardPresenter _gameboardPresenter;

    // Start is called before the first frame update
    void Start()
    {
        _gamestate = new Gamestate();

        CreatePeasant();
    }

    public void CreatePeasant()
    {
        Turret tester = new Peasant(_peasantType, new Vector2Int(5, 4), _gamestate);
        _gamestate.gameboard.AddTurret(tester);
        _gameboardPresenter.InitializeTurret(tester);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
