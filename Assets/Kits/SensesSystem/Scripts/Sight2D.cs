using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Sight2D : MonoBehaviour
{
    [SerializeField]  float radius = 5f;
    [SerializeField]  float checkFrequency = 5f;
    [SerializeField]  string targetTag = "Player";

    Transform closestTarget;
    float distanceToClosestTarget;

    float lastCheckTime;
    Collider2D[] colliders;
    void Update()
    {
        if ((Time.time - lastCheckTime) > (1f / checkFrequency))
        {
            lastCheckTime = Time.time;

            colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            closestTarget = null;
            distanceToClosestTarget = Mathf.Infinity;

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag(targetTag))
                {
                    float distanceToTarget = Vector3.Distance(transform.position, colliders[i].transform.position);
                    if (distanceToTarget < distanceToClosestTarget)
                    {
                        closestTarget = colliders[i].transform;
                        distanceToClosestTarget = distanceToTarget;
                    }
                }
            }
        }
    }

    public Transform GetClosestTarget()
    {
        return closestTarget;
    }

    public bool isTargetInSight()
    {
        bool isTargetDetected = false;

        if (!isTargetDetected && colliders != null && colliders.Length != 0)
        {
            foreach (var collider in colliders)
            {
                if (collider.CompareTag(targetTag))
                {
                    isTargetDetected = true;
                }
            }
        }

        return isTargetDetected;
    }
}
