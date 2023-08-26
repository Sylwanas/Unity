using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopPresenter : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _goldAmount;
    [SerializeField] private TurretType _turretType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _goldAmount.text = $"{_gameManager.gamestate.player.currentGold} GP";

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int position = new Vector2Int((int)mouseWorldPosition.x, (int)mouseWorldPosition.y);

            _gameManager.CreateTurret(_turretType, position);
        }
    }

    public void TurretSwitch(TurretType turretType)
    {
        _turretType = turretType;
    }
}
