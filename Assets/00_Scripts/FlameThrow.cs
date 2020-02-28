using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Collider2D))]
public class FlameThrow : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Trigger of FlameThrower detected");
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().HitEnemy(damage);
        }
    }
}
