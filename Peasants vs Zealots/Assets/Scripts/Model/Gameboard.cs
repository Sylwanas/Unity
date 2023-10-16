using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

[Serializable]
public class Gameboard
{
    private GameboardPresenter myPresenter;
    private Player myPlayer;

    [SerializeReference] private List<Unit> myUnits;
    public IEnumerable<Zealot> zealots => myUnits.Where(unit => unit is Zealot).Cast<Zealot>();
    public IEnumerable<Turret> turrets => myUnits.Where(unit => unit is Turret).Cast<Turret>();
    public int zealotCount => zealots.Count();
    [field: SerializeField] public int width { get; private set; }
    [field: SerializeField] public int height { get; private set; }

    public Gameboard(int width, int height) 
    { 
        this.width = width;
        this.height = height;
        myUnits = new List<Unit>();
    }

    public void InitializePlayer(Player player)
    {
        myPlayer = player;
    }

    public void InitializePresenter(GameboardPresenter presenter)
    {
        myPresenter = presenter;
    }

    public void DealDamage()
    {
        List<Unit> deadUnits = new List<Unit>();

        foreach (Zealot zealot in zealots)
        {
            int damage = zealot.zealotType.damage;

            foreach (Turret turret in turrets)
            {
                float distance = Math.Abs(zealot.position.x - turret.position.x);

                if (distance <= 0.5 && zealot.position.y == turret.position.y)
                {
                    zealot.attackCooldown(turret, damage);

                    if (turret.health == 0)
                    {
                        deadUnits.Add(turret);
                    }
                }
            }

            if (zealot.position.x <= 0)
            {
                myPlayer.ReactToDamage(damage);
                zealot.MoveBack();
            }
        }

        foreach (Unit deadUnit in deadUnits)
        {
            myUnits.Remove(deadUnit);
            myPresenter.DestroyUnit(deadUnit);
        }
    }

    public void RemoveUnit(Unit unit)
    {
        myUnits.Remove(unit);
    }
    public void AddUnit(Unit unit)
    {
        myUnits.Add(unit);
    }

    public void Update()
    {
        DealDamage();

        foreach (Unit unit in myUnits) 
        { 
            unit.Update();
        }
    }
}
