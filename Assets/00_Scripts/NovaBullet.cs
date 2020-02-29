using UnityEngine;
using System.Collections;

public class NovaBullet : Bullet
{
    protected override void Update()
    {
        worldPosition = transform.position;

        float timeInterval = Time.time - startTime;

        transform.position = Vector3.Lerp(StartPosition, TargetPosition, timeInterval * speed / distance);

        if (IsTargetPositionReached())
        {
            GetComponent<NovaController>().CreateImpact();
        }
    }

}
