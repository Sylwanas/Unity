using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopPresenter : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _goldAmount;
    [SerializeField] private TextMeshProUGUI _hpAmount;
    [SerializeField] private UnitType myUnitType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _goldAmount.text = $"{_gameManager.gameState.player.currentGold} GP";
        _hpAmount.text = $"{_gameManager.gameState.player.health}HP";

        if (Input.GetMouseButtonDown(0) && myUnitType != null)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int position = new Vector2Int(Mathf.FloorToInt(mouseWorldPosition.x), Mathf.FloorToInt(mouseWorldPosition.y));

            if (_gameManager.gameState.gameboard.IsTileEmpty(position) && !_gameManager.gameState.gameboard.IsTileOutside(position) &&_gameManager.gameState.player.health != 0)
            {
                _gameManager.CreateUnit(myUnitType, position);
            }
        }
    }

    public void TurretSwitch(TurretType turretType)
    {
        myUnitType = turretType;
    }
}
