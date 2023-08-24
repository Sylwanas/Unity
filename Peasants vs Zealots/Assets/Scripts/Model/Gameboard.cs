using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Gameboard
{
    [SerializeReference] private List<Turret> _turrets;
    [field: SerializeField] public int width { get; private set; }
    [field: SerializeField] public int height { get; private set; }

    public Gameboard(int width, int height) 
    { 
        this.width = width;
        this.height = height;
        _turrets = new List<Turret>();
    }

    public void AddTurret(Turret turret)
    {
        _turrets.Add(turret);
    }

    public void Update()
    {
        foreach (Turret turret in _turrets) 
        { 
            turret.Update();
        }
    }
}
