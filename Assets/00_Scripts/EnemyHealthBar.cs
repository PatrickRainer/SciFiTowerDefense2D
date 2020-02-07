using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float currentHealth = 100;

    private Enemy myEnemy;

    private float originalScale;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    private void Start()
    {
        myEnemy = GetComponentInParent<Enemy>();
        maxHealth = myEnemy.GetHealth();
        originalScale = transform.localScale.x;
    }

    private void Update()
    {
        currentHealth = myEnemy.GetHealth();

        Vector3 tmpScale = transform.localScale;
        tmpScale.x = CurrentHealth / maxHealth * originalScale;
        transform.localScale = tmpScale;
    }
}
