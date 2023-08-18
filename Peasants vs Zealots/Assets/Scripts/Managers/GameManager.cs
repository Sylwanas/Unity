using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Gamestate gamestate = new Gamestate();
        GameboardPresenter gameboardPresenter = transform.GetComponentInChildren<GameboardPresenter>();
        gameboardPresenter.Initialize(gamestate.gameboard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
