using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    public LayerMask playerLayer;
    public float xMagnitude;
    public float distance;

    public float fireDuration;
    float fireTime;
    public float noFireDuration;
    float noFireTime;

    private EnemyPatrolController _enemyPatrol;
    public ProjectileGenerator _projectileGenerator;

    private void Awake()
    {
        _enemyPatrol = this.GetComponent<EnemyPatrolController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        fireTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D playerInfo = Physics2D.Raycast(this.transform.position, new Vector2(_enemyPatrol.Dir, 0), distance, playerLayer);

        if (playerInfo)
        {
            _enemyPatrol.CanMove = false;
            FireProjectiles();
        }
        else
        {
            _enemyPatrol.CanMove = true;
            fireTime = fireDuration;
        }
    }

    void FireProjectiles()
    {
        _projectileGenerator.directionToShoot = new Vector2(_enemyPatrol.Dir * xMagnitude, _projectileGenerator.directionToShoot.y);
        if (fireTime <= fireDuration)
        {
            fireTime += Time.deltaTime;
            noFireTime = 0;
            _projectileGenerator.LaunchProjectile();
        }
        else
        {
            if (noFireTime <= noFireDuration)
            {
                noFireTime += Time.deltaTime;
            }
            else
            {
                fireTime = 0;
            }
        }
    }
}
