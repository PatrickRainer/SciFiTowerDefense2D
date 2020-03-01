using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileTower : Tower
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void Shoot(GameObject target)
    {
        if (target != null)
        {
            if ((Time.time - lastShotTime) > FireRate)
            {
                GameObject bulletPrefab = Bullet;

                GameObject missileObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Missile newMissile = missileObj.GetComponent<Missile>();
                newMissile.enemyToFollow = target;
                //newMissile.damage = bulletDamage;

                lastShotTime = Time.time;
            }
        }
    }
}
