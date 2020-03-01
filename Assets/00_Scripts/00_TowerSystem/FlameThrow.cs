using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Collider2D)), RequireComponent(typeof(AudioSource))]
public class FlameThrow : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private void OnEnable()
    {
        GetComponent<AudioSource>().Play();
    }

    private void OnDisable()
    {
        GetComponent<AudioSource>().Stop();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Trigger of FlameThrower detected");
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().HitEnemy(damage);
        }
    }
}
