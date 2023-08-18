using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameboardPresenter : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Gameboard gameboard)
    {
        for (int x = 0;  x < gameboard.width; x++) 
        { 
            for (int y = 0; y < gameboard.height; y++) 
            { 
                Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
            }
        }
    }
}
