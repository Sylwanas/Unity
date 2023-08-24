using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretPresenter : MonoBehaviour
{
    private Turret _turret;
    public void Initialize(Turret turret)
    {
        _turret = turret;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = turret.type.turretSprite;
    }

    private void Update()
    {
        transform.localPosition = new Vector3(_turret.position.x, _turret.position.y, 0);
    }
}
