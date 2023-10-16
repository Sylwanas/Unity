using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameboardPresenter : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject unitPrefab;

    Dictionary<Unit, GameObject> unitGameObjects = new Dictionary<Unit, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeUnit(Unit unit) 
    {
        Transform unitsTransform = transform.Find("Units");
        GameObject unitGameObject = Instantiate(unitPrefab, unitsTransform);

        unitGameObjects[unit] = unitGameObject;

        UnitPresenter unitPresenter = unitGameObject.GetComponent<UnitPresenter>();
        unitPresenter.Initialize(unit);
    }

    public void DestroyUnit(Unit unit) 
    {
        GameObject unitGameObject = unitGameObjects[unit];
        Destroy(unitGameObject);
        unitGameObjects.Remove(unit);
    }

    public void Initialize(Gameboard gameboard)
    {
        Transform tilesTransform = transform.Find("Tiles");

        for (int x = 0;  x < gameboard.width; x++) 
        { 
            for (int y = 0; y < gameboard.height; y++) 
            { 
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity, tilesTransform);
                tile.name = $"Tile {x}, {y}";
            }
        }
    }
}
