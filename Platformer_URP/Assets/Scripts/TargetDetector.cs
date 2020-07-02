using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public LayerMask targetLayer;
    [Range(1, 20)] [SerializeField] private float m_DetectDistance = 10f;

    public GameObject DetectTarget()
    {
        int targetIndex = 99;
        float shortestDist = 999f;
        GameObject target= null;
        RaycastHit2D[] hit = Physics2D.CircleCastAll(this.transform.position, m_DetectDistance, Vector2.right, m_DetectDistance * 2f, targetLayer);
        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider != null)
                {
                    float dist = Vector3.Distance(this.transform.position, hit[i].collider.gameObject.transform.position);
                    if (dist < shortestDist)
                    {
                        targetIndex = i;
                        shortestDist = dist;
                    }
                }
            }

            if (targetIndex < hit.Length)
            {
                target = hit[targetIndex].collider.gameObject;
            }
        }
        else
        {
            target = null;
        }

        return target;
    }
}
