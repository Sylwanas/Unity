using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPresenter : MonoBehaviour
{
    [SerializeField] private PeasantType _peasantType;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _goldAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void CreatePeasant(Vector2Int position)
    {
        _gameManager.CreatePeasant(_peasantType, position);
    }

    // Update is called once per frame
    void Update()
    {
        _goldAmount.text = $"{_gameManager.gamestate.player.currentGold} GP";

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int tileCoordinates = new Vector2Int((int)mouseWorldPosition.x, (int)mouseWorldPosition.y);
            CreatePeasant(tileCoordinates);
        }
    }
}
