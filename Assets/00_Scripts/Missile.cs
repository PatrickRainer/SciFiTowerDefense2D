using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class Missile : MonoBehaviour
{
    [SerializeField]
    float speed = 3;
    [SerializeField]
    public int damage = 50;

    public GameObject enemyToFollow;
       
    void Start()
    {
        FollowGameObject(enemyToFollow);
    }

    private void FollowGameObject(GameObject target)
    {
        if (target == null)
        {
            return;
        }

        Vector3 v3 = (target.transform.position - transform.position).normalized;
        Vector2 v2 = new Vector2(v3.x, v3.y);
        GetComponent<Rigidbody2D>().velocity = v2 * speed;
    }

    private void FixedUpdate()
    {
        FollowGameObject(enemyToFollow);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == enemyToFollow)
        {
            //Debug.Log("MyNova collided with the target");
            collision.gameObject.GetComponent<Enemy>().HitEnemy(damage);
            GetComponent<NovaController>().CreateImpact();
        }
    }
}

