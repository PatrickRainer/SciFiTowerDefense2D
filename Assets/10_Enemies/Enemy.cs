using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;
    [SerializeField,ReadOnly]
    private GameObject lastWaypoint;
    [SerializeField, Tooltip("The Range when the enemy changes to the next waypoint.")]
    private float rotationRange = 2;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private float rotationDuration = 0.5f;

    private int currentWaypointIndex = 0;
    private float lastWaypointReachedTime;

    private NavMeshAgent2D navMeshAgent2D;
    private CameraShake cameraShake;

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int value)
    {
        health = value;
        if (health <= 0)
        {
            EventManager.RaiseEnemyDestroyed();
            Destroy(gameObject);
        }
    }

    public GameObject[] Waypoints { get => waypoints; set => waypoints = value; }

    private void Awake()
    {
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
        navMeshAgent2D = GetComponent<NavMeshAgent2D>();

        gameObject.tag = "Enemy";
    }

    private void Start()
    {
        lastWaypoint = waypoints[waypoints.Length - 1];
        lastWaypointReachedTime = Time.time;
        //Move();
        navMeshAgent2D.speed *= speed;
        MoveWithAgent();
        StartCoroutine(SetDirection());
    }

    private void Move()
    {
        Vector3[] path = new Vector3[waypoints.Length];
        for (int i = 0; i < Waypoints.Length; i++)
        {
            path[i] = Waypoints[i].GetPosition();
        }

        transform.DOPath(path, speed * 10, PathType.Linear, PathMode.TopDown2D);
    }

    void MoveWithAgent()
    {
        navMeshAgent2D.enabled = true;
        navMeshAgent2D.SetDestination(lastWaypoint.GetPosition());
    }

    private IEnumerator SetDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);

            Vector3 lastPosition = transform.position;

            yield return new WaitForSeconds(0.02f);

            Vector3 direction = transform.position - lastPosition;
            //Debug.Log(direction);

            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void Update()
    {
        WaypointHandling();
    }

    private void WaypointHandling()
    {
        Vector3 curWPpos = waypoints[currentWaypointIndex].transform.position;

            if (IsLastWaypoint())
            {
                //Debug.Log("LastWaypoint reached");
                LevelManager.PlayerHealth -= 1;
                cameraShake.StartShake();
                Destroy(gameObject);
            }
            else if (IsInRangeOf(curWPpos))
            {
                currentWaypointIndex++;
                curWPpos = waypoints[currentWaypointIndex].transform.position;
                //LookAt(curWPpos, transform.position, transform);  // Handled in SetDirection
                lastWaypointReachedTime = Time.time;
            }
    }

    private bool IsLastWaypoint()
    {
        float distance = Vector3.Distance(transform.position, lastWaypoint.GetPosition());
        return distance < 1;
    }

    //compares the waypoint exactly
    private bool EnemyReachedWaypoint(Vector3 waypointPosition)
    {
        if (transform.position.Equals(waypointPosition))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Compares the waypoint with some range tolerance
    //https://answers.unity.com/questions/540120/how-do-you-update-navmesh-rotation-after-stopping.html
    private bool IsInRangeOf(Vector3 targetPos)
    {
        float distance = Vector3.Distance(transform.position, targetPos);
        return distance < rotationRange;
    }

    //private void RotateTowards(Transform target)
    //{
    //    Vector3 direction = (target.position - transform.position).normalized;
    //    Quaternion lookRotation = Quaternion.LookRotation(direction);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    //}

    public void LookAt(Vector3 targetPos, Vector3 sourcePos, Transform transformToRotate)
    {
        Vector3 distance = targetPos - sourcePos;
        float rotZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        transformToRotate.transform.DOLocalRotate(new Vector3(0, 0, rotZ), rotationDuration).Play();
    }

    public float GetDistanceToEndGoal()
    {
        float distance = 0;
        distance += Vector2.Distance(transform.position, waypoints[currentWaypointIndex + 1].transform.position);

        for (int i = currentWaypointIndex;  i < waypoints.Length -1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }

        return distance;
    }

    public void HitEnemy(int damage)
    {
        SetHealth(GetHealth() - damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //Debug.Log("Enemy is Hit");
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            HitEnemy(bullet.Damage);
        }
    }



















}
