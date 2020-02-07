using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Camera))]
public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float shakeDuration = 0f;
    [SerializeField]
    private float shakeAmount = 0.7f;
    [SerializeField]
    private float decreaseFactor = 1.0f;
    [SerializeField]
    private float shakeSaveDurationValue = 0f;

    private Vector3 originalPos;

    private void OnEnable()
    {
        originalPos = transform.localPosition;
    }

    [ResponsiveButtonGroup]
    public void StartShake()
    {
        shakeDuration = shakeSaveDurationValue;
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        if (GameStatusManager.Status != GameStates.GameRuns)
        {
            shakeDuration = 0;
        }

        if (shakeDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0;
            transform.localPosition = originalPos;
        }
    }
}
