using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossStage
{
    Lvl1,
    Lvl2,
    Lvl3,
    Lvl4,
    Lvl5
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
    private int lastFacingDireciton = 1;
    [SerializeField] ParticleSystem ps_poweringUp;
    [SerializeField] ParticleSystem ps_Release;
    private EnemyPatrolController _enemyPatrol;
    private EnemyUnitController _enemyUnit;

    public BossAreaSpikeController arenaSpikeController;
    public EnemyUnitController EnemyUnit { get => _enemyUnit;}

    private void Awake()
    {
        _enemyPatrol = this.GetComponent<EnemyPatrolController>();
        _enemyUnit = this.GetComponent<EnemyUnitController>();
        arenaSpikeController.ps_poweringUp = this.ps_poweringUp;
        arenaSpikeController.ps_Release = this.ps_Release;

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyUnit.invulnerable || _enemyPatrol.CanMove)
        {
            ps_poweringUp.gameObject.SetActive(false);
            ps_Release.gameObject.SetActive(false);
        }

        CheckBossStage();
        DecideEpicenter();
    }

    public int DetectPlayerDirection()
    {
        if (_enemyUnit.invulnerable)
        {
            return lastFacingDireciton;
        }

        int targetIndex = 99;
        float shortestDist = 999f;

        RaycastHit2D[] hit = Physics2D.CircleCastAll(this.transform.position, detectDistance, Vector2.right, detectDistance * 2f, playerLayer);
        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider != null && hit[i].collider.CompareTag("Player"))
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
                {
                    lastFacingDireciton = 1;
                }
                else
                {
                    lastFacingDireciton = - 1;
                }
            }

        }
        return lastFacingDireciton;
    }

    void CheckBossStage()
    {
        if (_enemyUnit.currentHealth == 5)
            stage = BossStage.Lvl1;
        else if (_enemyUnit.currentHealth == 4)
            stage = BossStage.Lvl2;
        else if (_enemyUnit.currentHealth == 3)
            stage = BossStage.Lvl3;
        else if (_enemyUnit.currentHealth == 2)
            stage = BossStage.Lvl4;
        else if (_enemyUnit.currentHealth == 1)
            stage = BossStage.Lvl5;
    }

    void DecideEpicenter()
    {
        for (int i = 0; i < arenaSpikeController.spikeList.Count; i++)
        {
            if (Vector2.Distance(this.transform.position, arenaSpikeController.spikeList[i].transform.position) <= 0.3f)
            {
                arenaSpikeController.epicenterSpikeIndex = i;
            }
        }
    }
}
