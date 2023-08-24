using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameboardPresenter : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject turretPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeTurret(Turret turret) 
    {
        Transform turretsTransform = transform.Find("Turrets");
        GameObject turretGameObject = Instantiate(turretPrefab, turretsTransform);
        TurretPresenter turretPresenter = turretGameObject.GetComponent<TurretPresenter>();
        turretPresenter.Initialize(turret);
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
