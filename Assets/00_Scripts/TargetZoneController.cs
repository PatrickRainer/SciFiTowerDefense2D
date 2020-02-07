using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Collider2D))]
public class TargetZoneController : MonoBehaviour
{
    private CameraShake cameraShake;
    private Collider2D myCollider;

    private void Awake()
    {
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObj = collision.gameObject;

        if (otherObj.tag == "Enemy")
        {
            //Debug.Log("Targetzone reached");
            LevelManager.PlayerHealth -= 1;
            cameraShake.StartShake();
            Destroy(otherObj);
        }
    }
}
