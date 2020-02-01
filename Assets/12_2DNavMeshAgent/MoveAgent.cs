using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAgent : MonoBehaviour
{

    public GameObject waypoint;

    private void Start()
    {
        GetComponent<NavMeshAgent2D>().SetDestination(waypoint.transform.position);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 w = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<NavMeshAgent2D>().destination = w;
        }
    }
}
