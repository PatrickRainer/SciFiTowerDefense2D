using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using DG.Tweening;

public enum TowerGroup { Tanks, Group2 }
public enum Tier { Tier1, Tier2, Tier3 }
public enum TowerTypes { Laser, Flamer, Atom}


public class Tower : MonoBehaviour
{
    [SerializeField, InlineButton("GenerateID")]
    private string id;
    [SerializeField]
    public string title;
    [SerializeField]
    private int cost;
    public int resellPrice;
    [EnumToggleButtons, Required]
    public TowerTypes type;
    [Required, Range(0, 2)]
    public int upgradeLevel;
    [SerializeField, AssetsOnly,Required]
    private GameObject bullet;
    [SerializeField]
    private float fireRate = 1;
    [SerializeField]
    private float fireRange = 3;
    [SerializeField]
    private float turnRate = 25f;
    [SerializeField, ReadOnly]
    private SocketController myTowerSpot;

    [SerializeField, ReadOnly, SceneObjectsOnly]
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private float lastShotTime;
    private GameObject targetZone;
    private GameObject targetToShoot;


    public string ID { get => id; set => id = value; }
    public GameObject Bullet { get => bullet; set => bullet = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int Cost { get => cost; set => cost = value; }

    private void Start()
    {
        myTowerSpot = GetComponentInParent<SocketController>();
        lastShotTime = Time.time;
        GetComponent<CircleCollider2D>().radius = fireRange;
        targetZone = GameObject.FindObjectOfType<TargetZoneController>().gameObject;
    }

    private void GenerateID()
    {
        ID = System.Guid.NewGuid().ToString();
    }

    private void Update()
    {
        targetToShoot = GetNearestEnemyToTargetZone();
        RotateTower();
        Shoot(targetToShoot);
    }

    //private GameObject GetNearestEnemyToTower()
    //{
    //    GameObject nearestEnemy = null;
    //    float minimalEnemyDistance = float.MaxValue;
    //    float distanceBetweenEnemy = 0;

    //    foreach (GameObject enemy in enemiesInRange)
    //    {
    //        distanceBetweenEnemy = Vector2.Distance(enemy.GetPosition(), gameObject.GetPosition());

    //        if (distanceBetweenEnemy < minimalEnemyDistance)
    //        {
    //            nearestEnemy = enemy;
    //            minimalEnemyDistance = distanceBetweenEnemy;
    //        }
    //    }
    //    return nearestEnemy;
    //}

    private GameObject GetNearestEnemyToTargetZone()
    {
        GameObject nearest = null;
        float minimalEnemyDistance = float.MaxValue;
        float distanceBetweenEnemy = 0;

        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToTargetZone = Vector2.Distance(targetZone.GetPosition(), enemy.GetPosition());

            if (distanceToTargetZone < minimalEnemyDistance)
            {
                nearest = enemy;
                minimalEnemyDistance = distanceBetweenEnemy;
            }
        }

        return nearest;
    }

    private void Shoot(GameObject target)
    {
        if (target != null)
        {
            if ((Time.time - lastShotTime) > FireRate)
            {
                GameObject bulletPrefab = Bullet;
                Vector3 targetPosition = target.transform.position;

                GameObject go = Instantiate(bulletPrefab, transform);
                Bullet newBullet = go.GetComponent<Bullet>();

                newBullet.Target = target;
                newBullet.TargetPosition = targetPosition;
                newBullet.StartPosition = transform.position;
                                                                      
                lastShotTime = Time.time;
            }
        }
    }

    private void RotateTower()
    {
        if (targetToShoot == null)
        {
            return;
        }

        transform.right = Vector3.Slerp(transform.right, (targetToShoot.GetPosition() - transform.position), Time.deltaTime * turnRate);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //print("Enemy entered Tower-Trigger");
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



}
