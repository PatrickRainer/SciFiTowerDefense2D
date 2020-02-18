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
    private float speed = 10;
    [SerializeField]
    private int damage;
    [SerializeField]
    private AudioClip bulletSound;

    [HorizontalGroup("Base")]
    [VerticalGroup("Base/Left")]
    [SerializeField, ReadOnly, PreviewField(ObjectFieldAlignment.Left), HideLabel]
    private GameObject target;
    [VerticalGroup("Base/Right")]
    [SerializeField, ReadOnly]
    private Vector3 startPosition;
    [SerializeField, ReadOnly]
    [VerticalGroup("Base/Right")]
    private Vector3 targetPosition;
    [SerializeField, ReadOnly]
    [VerticalGroup("Base/Right")]
    private Vector3 worldPosition;
    [ReadOnly]
    [VerticalGroup("Base/Right")]
    public Tower myTower;

    private float distance;
    private float startTime;

    public GameObject Target { get => target; set => target = value; }
    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }
    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
    public int Damage { get => damage; set => damage = value; }

    private void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(TargetPosition, StartPosition);
        AudioSource.PlayClipAtPoint(bulletSound, gameObject.GetPosition());
    }

    private void Update()
    {
        worldPosition = transform.position;

        float timeInterval = Time.time - startTime;

        transform.position = Vector3.Lerp(StartPosition, TargetPosition, timeInterval * speed / distance);

        if (IsTargetPositionReached())
        {
            Destroy(gameObject);
        } 
    }

    private bool IsTargetPositionReached()
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
