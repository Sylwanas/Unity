using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PeasantType _peasantType;

    [SerializeField] private Gamestate _gamestate;

    // Start is called before the first frame update
    void Start()
    {
        _gamestate = new Gamestate();

        GameboardPresenter gameboardPresenter = transform.GetComponentInChildren<GameboardPresenter>();
        gameboardPresenter.Initialize(_gamestate.gameboard);
    }

    // Update is called once per frame
    void Update()
    {
        _gamestate.Update();
    }
}
