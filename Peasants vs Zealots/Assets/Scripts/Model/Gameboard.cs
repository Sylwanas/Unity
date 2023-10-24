using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

[Serializable]
public class Gameboard
{
    private GameboardPresenter myPresenter;
    private Player myPlayer;
    private List<Unit> myAddedUnits = new List<Unit>();

    [field: SerializeField] public List<Unit> Units { get; private set; }
    public IEnumerable<Zealot> zealots => Units.Where(unit => unit is Zealot).Cast<Zealot>();
    public IEnumerable<Turret> turrets => Units.Where(unit => unit is Turret).Cast<Turret>();
    public IEnumerable<Soldier> soldiers => Units.Where(unit => unit is Soldier).Cast<Soldier>();
    public IEnumerable<Arrow>  arrows => Units.Where(unit => unit is Arrow).Cast<Arrow>();
    public int zealotCount => zealots.Count();
    [field: SerializeField] public int width { get; private set; }
    [field: SerializeField] public int height { get; private set; }

    public Gameboard(int width, int height) 
    { 
        this.width = width;
        this.height = height;
        Units = new List<Unit>();
    }

    public void InitializePlayer(Player player)
    {
        myPlayer = player;
    }

    public void InitializePresenter(GameboardPresenter presenter)
    {
        myPresenter = presenter;
    }

    public float CalculateDistanceBetweenUnits(Unit unit1, Unit unit2)
    {
        return Math.Abs(unit1.position.x - unit2.position.x);
    }

    public bool IsTargetInRange(Unit attacker, Unit target)
    {
        IRangeProvider attackRangeProvider = attacker as IRangeProvider;

        return (CalculateDistanceBetweenUnits(attacker, target) <= attackRangeProvider.attackRange && attacker.position.y == target.position.y);
    }

    public bool AreUnitsInContact(Unit unit1, Unit unit2)
    {
        return CalculateDistanceBetweenUnits(unit1, unit2) <= 0.5 && unit1.position.y == unit2.position.y;
    }

    public void DealDamage()
    {
        List<Unit> deadUnits = new List<Unit>();

        foreach (Zealot zealot in zealots)
        {
            foreach (Turret turret in turrets)
            {
                if (IsTargetInRange(zealot, turret))
                {
                    zealot.Attack(turret);

                    if (turret.health == 0)
                    {
                        deadUnits.Add(turret);
                    }
                }
            }

            if (zealot.position.x <= 0)
            {
                zealot.Attack(myPlayer);
            }
        }

        foreach (Arrow arrow in arrows)
        {
            foreach (Zealot zealot in zealots)
            {
                if (arrow.position.x >= 12.5)
                {
                    deadUnits.Add(arrow);
                }

                if (IsTargetInRange(arrow, zealot))
                {
                    arrow.Attack(zealot);
                    deadUnits.Add(arrow);

                    if (zealot.health == 0)
                    {
                        deadUnits.Add(zealot);
                    }
                }
            }
        }

        foreach (Soldier soldier in soldiers)
        {
            foreach (Zealot zealot in zealots)
            {
                if (IsTargetInRange(soldier, zealot))
                {
                    soldier.Attack(zealot);

                    if (zealot.health == 0)
                    {
                        deadUnits.Add(zealot);
                    }
                }
            }
        }

        foreach (Unit deadUnit in deadUnits)
        {
            Units.Remove(deadUnit);
            myPresenter.DestroyUnit(deadUnit);
        }
    }

    public bool IsTileEmpty(Vector2Int position)
    {
        foreach (Unit unit in Units)
        {
            if (unit.tilePosition == position)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsTileOutside(Vector2Int position)
    {
        return position.x < 0 || position.x >= width || position.y < 0 || position.y >= height;
    }

    public void RemoveUnit(Unit unit)
    {
        Units.Remove(unit);
    }
    public void AddUnit(Unit unit)
    {
        myAddedUnits.Add(unit);
    }

    public void Update()
    {
        DealDamage();

        foreach (Unit unit in Units) 
        { 
            unit.Update();
        }

        Units.AddRange(myAddedUnits);
        myAddedUnits.Clear();
    }
}
