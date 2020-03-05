using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 10f;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }
}
