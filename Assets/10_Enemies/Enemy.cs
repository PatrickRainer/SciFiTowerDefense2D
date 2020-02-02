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
    private float speed = 1.0f;
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private float rotationDuration = 0.5f;
    [SerializeField]
    private GameObject navMeshTarget;
    [SerializeField]
    private GameObject explosionPrefab;

    private NavMeshAgent2D navMeshAgent2D;
    private CameraShake cameraShake;

    public GameObject NavMeshTarget { get => navMeshTarget; set => navMeshTarget = value; }

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

    private void Awake()
    {
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
        navMeshAgent2D = GetComponent<NavMeshAgent2D>();

        gameObject.tag = "Enemy";
    }

    private void Start()
    {
        //Move();
        navMeshAgent2D.speed *= speed;
        MoveWithAgent();
        StartCoroutine(SetDirection());
    }

    void MoveWithAgent()
    {
        navMeshAgent2D.enabled = true;
        navMeshAgent2D.SetDestination(navMeshTarget.GetPosition());
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

    private void OnDestroy()
    {

       
        if (explosionPrefab == null) { return; }
        Debug.Log("ondestroy");
        Instantiate(explosionPrefab, gameObject.GetPosition(),Quaternion.identity);
    }



















}
