using UnityEngine;
using System.Collections;

public class FlameTower : Tower
{
    private GameObject myFlameThrow = null;

    protected override void Start()
    {
        base.Start();

        GameObject flamePrefab = Bullet;
        myFlameThrow = Instantiate(flamePrefab, transform);

        myFlameThrow.SetActive(false);
    }

    protected override void Update()
    {
        targetToShoot = GetNearestEnemyToTargetZone();
        RotateTower();

        if (enemiesInRange.Count > 0)
        {
            myFlameThrow.SetActive(true);
        }
        else
        {
            myFlameThrow.SetActive(false);
        }
    }
}
