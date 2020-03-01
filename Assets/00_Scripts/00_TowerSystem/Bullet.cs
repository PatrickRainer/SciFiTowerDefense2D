using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

static class ExtensionMethods
{
    /// <summary>
    /// Rounds Vector3.
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="decimalPlaces"></param>
    /// <returns></returns>
    public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 2)
    {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++)
        {
            multiplier *= 10f;
        }
        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }
}



public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    protected float speed = 10;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected AudioClip bulletSound;

    [HorizontalGroup("Base")]
    [VerticalGroup("Base/Left")]
    [SerializeField, ReadOnly, PreviewField(ObjectFieldAlignment.Left), HideLabel]
    protected GameObject target;
    [VerticalGroup("Base/Right")]
    [SerializeField, ReadOnly]
    protected Vector3 startPosition;
    [SerializeField, ReadOnly]
    [VerticalGroup("Base/Right")]
    protected Vector3 targetPosition;
    [SerializeField, ReadOnly]
    [VerticalGroup("Base/Right")]
    protected Vector3 worldPosition;
    [ReadOnly]
    [VerticalGroup("Base/Right")]
    public Tower myTower;

    protected float distance;
    protected float startTime;

    public GameObject Target { get => target; set => target = value; }
    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }
    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
    public int Damage { get => damage; set => damage = value; }

    protected virtual void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(TargetPosition, StartPosition);
        if (bulletSound != null)
        {
            AudioSource.PlayClipAtPoint(bulletSound, gameObject.GetPosition());
        }      
    }

    protected virtual void Update()
    {
        worldPosition = transform.position;

        float timeInterval = Time.time - startTime;

        transform.position = Vector3.Lerp(StartPosition, TargetPosition, timeInterval * speed / distance);

        if (IsTargetPositionReached())
        {
            Destroy(gameObject);
        } 
    }

    protected virtual bool IsTargetPositionReached()
    {
        // Fixed Bug: Need to round the vectors, because else the enemy is sometimes just a pont later
        // and the compare of the two vectors does not work then
        if (transform.position.Round(4).Equals(TargetPosition.Round(4)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
