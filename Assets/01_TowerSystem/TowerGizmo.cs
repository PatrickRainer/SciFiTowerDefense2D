using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGizmo : MonoBehaviour
{
    [SerializeField]
    private float gizmoRange = 3;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, gizmoRange);
        }
}
