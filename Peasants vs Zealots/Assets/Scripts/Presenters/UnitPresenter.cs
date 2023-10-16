using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UnitPresenter : MonoBehaviour
{
    private Unit myUnit;
    public void Initialize(Unit unit)
    {
        myUnit = unit;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = unit.type.sprite;
    }

    private void Update()
    {
        transform.localPosition = new Vector3(myUnit.position.x, myUnit.position.y, 0);
    }
}
