using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

[RequireComponent(typeof(Tower))]
public class FireTowerBullet : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemiesInRange = new List<GameObject>();

    private float lastShotTime;
    private Tower tower;

    private void Start()
    {
        lastShotTime = Time.time;
        tower = GetComponent<Tower>();
    }

    private void Update()
    {
        GameObject target = null;
        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<Enemy>().GetDistanceToEndGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }

        if (target != null)
        {
            if (Time.time - lastShotTime > tower.FireRate)
            {
                Shoot(target);
                lastShotTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print ("Collider Trigger Enter");

        if (other.gameObject.tag == "Enemy")
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    private void Shoot(GameObject target)
    {
        GameObject bulletPrefab = tower.Bullet;
        Vector3 targetPosition = target.transform.position;

        GameObject go = Instantiate(bulletPrefab);
        Bullet tb = go.GetComponent<Bullet>();

        tb.Target = target;
        tb.StartPosition = transform.position;
    }
}
