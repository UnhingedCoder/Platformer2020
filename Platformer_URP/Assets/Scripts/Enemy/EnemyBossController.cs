using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossStage
{
    Lvl1,
    Lvl2,
    Lvl3
}
public class EnemyBossController : MonoBehaviour
{
    public LayerMask playerLayer;
    public BossStage stage;
    public int projectilesToShoot;
    public float detectDistance = 100f;
    public float attackRange = 10f;
    public GameObject playerTarget;
    private Vector3 playerDirection;

    private EnemyPatrolController _enemyPatrol;
    private EnemyProjectileController _enemyProjectile;
    private EnemyUnitController _enemyUnit;

    public EnemyProjectileController EnemyProjectile { get => _enemyProjectile;}
    public EnemyUnitController EnemyUnit { get => _enemyUnit;}

    private void Awake()
    {
        _enemyPatrol = this.GetComponent<EnemyPatrolController>();
        _enemyProjectile = this.GetComponent<EnemyProjectileController>();
        _enemyUnit = this.GetComponent<EnemyUnitController>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckBossStage();
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

    void CheckBossStage()
    {
        if (_enemyUnit.currentHealth >= _enemyUnit.totalHealth)
            stage = BossStage.Lvl1;
        else if (_enemyUnit.currentHealth <= _enemyUnit.totalHealth / 1.5)
            stage = BossStage.Lvl2;
        else if (_enemyUnit.currentHealth < _enemyUnit.totalHealth / 2)
            stage = BossStage.Lvl3;

        AssignProjectiles();
    }

    void AssignProjectiles()
    {
        switch (stage)
        {
            case BossStage.Lvl1:
                {
                    projectilesToShoot = (int)(_enemyProjectile.xMagnitude.Length * 0.6);
                }
                break;

            case BossStage.Lvl2:
                {
                    projectilesToShoot = (int)(_enemyProjectile.xMagnitude.Length * 0.9);
                }
                break;

            case BossStage.Lvl3:
                {
                    projectilesToShoot = (int)(_enemyProjectile.xMagnitude.Length);
                }
                break;
        }
    }
}
