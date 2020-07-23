using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : MonoBehaviour
{
    public LayerMask playerLayer;
    public float detectDistance = 100f;
    public float attackRange = 10f;
    public GameObject playerTarget;
    private Vector3 playerDirection;

    private EnemyPatrolController _enemyPatrol;
    public EnemyProjectileController _enemyProjectile;

    private void Awake()
    {
        _enemyPatrol = this.GetComponent<EnemyPatrolController>();
        _enemyProjectile = this.GetComponent<EnemyProjectileController>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int DetectPlayerDirection()
    {
        int targetIndex = 99;
        float shortestDist = 999f;

        RaycastHit2D[] hit = Physics2D.CircleCastAll(this.transform.position, detectDistance, Vector2.right, detectDistance * 2f, playerLayer);
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
                playerTarget = hit[targetIndex].collider.gameObject;
                playerDirection = (playerTarget.transform.position - this.transform.position).normalized;

                if (playerDirection.x > 0)
                    return 1;
                else
                    return -1;
            }
        }
        return 1;
    }
}
