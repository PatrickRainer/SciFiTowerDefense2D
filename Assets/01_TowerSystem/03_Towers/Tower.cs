using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using DG.Tweening;

public enum TowerGroup { Tanks, Group2 }
public enum Tier { Tier1, Tier2, Tier3 }

public static class GameObjectExtension
{
    /// <summary>
    /// Returns the World Position of the gameObject
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static Vector3 GetPosition(this GameObject go)
    {
        return go.transform.position;
    }
}

public class Tower : MonoBehaviour
{
    [SerializeField, InlineButton("GenerateID")]
    private string id;
    [SerializeField]
    private string title;
    [SerializeField]
    private int cost;
    [SerializeField, EnumToggleButtons]
    private int groupID;
    [SerializeField, EnumToggleButtons]
    private int tierID;
    [SerializeField, AssetsOnly,Required]
    private GameObject bullet;
    [SerializeField]
    private float fireRate = 1;
    [SerializeField, ReadOnly]
    private TowerSpot myTowerSpot;

    [SerializeField, ReadOnly, SceneObjectsOnly]
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private float lastShotTime;
    //private Tower myTower;
    private GameObject nearestTarget;


    public string ID { get => id; set => id = value; }
    public int GroupID { get => groupID; set => groupID = value; }
    public int TierID { get => tierID; set => tierID = value; }
    public GameObject Bullet { get => bullet; set => bullet = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int Cost { get => cost; set => cost = value; }

    private void Start()
    {
        myTowerSpot = GetComponentInParent<TowerSpot>();
        lastShotTime = Time.time;
    }

    private void GenerateID()
    {
        ID = System.Guid.NewGuid().ToString();
    }

    private void Update()
    {
        SetNearestTarget();
        RotateTowerToNearestTarget();
        Shoot(nearestTarget);
    }

    private void SetNearestTarget()
    {
        nearestTarget = null;
        float minimalEnemyDistance = float.MaxValue;
        float distanceBetweenEnemy = 0;

        foreach (GameObject enemy in enemiesInRange)
        {
            distanceBetweenEnemy = Vector2.Distance(enemy.GetPosition(), gameObject.GetPosition());

            // This would set the enemy which is nearest to the targetzone
            //float distanceToGoal = enemy.GetComponent<Enemy>().GetDistanceToEndGoal();

            if (distanceBetweenEnemy < minimalEnemyDistance)
            {
                nearestTarget = enemy;
                minimalEnemyDistance = distanceBetweenEnemy;
            }
        }
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

    private void RotateTowerToNearestTarget()
    {
        if (nearestTarget == null)
        {
            return;
        }

        Vector3 direction = transform.position - nearestTarget.GetPosition();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.position, nearestTarget.GetPosition() - transform.position), Time.deltaTime*10);

        //transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI, new Vector3(0, 0, 1));
        //transform.rotation *= Quaternion.Euler(0, 0, 0); // Would add some additional rotation if necessary
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
