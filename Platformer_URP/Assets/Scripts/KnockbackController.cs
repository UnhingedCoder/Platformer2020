using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackController : MonoBehaviour
{
    public float damage;
    public float knockbackDuration;
    public bool shouldDamageEnemies;
    public bool destroySelf;
    public Vector2 burstOffset;
    public ParticleSystem burstFX;
    private PlayerController player;
    private CameraController _camController;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        _camController = FindObjectOfType<CameraController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KnockbackPlayer(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KnockbackPlayer(collision);
        }

        if (shouldDamageEnemies)
        {
            if (collision.CompareTag("EnemyHead") || collision.CompareTag("Enemy"))
            {
                EnemyUnitController enemyUnit = collision.transform.parent.GetComponent<EnemyUnitController>();
                GameObject fx = Instantiate(enemyUnit.ps_damageTaken.gameObject, new Vector3(this.transform.position.x, this.transform.position.y, 0), enemyUnit.ps_damageTaken.transform.rotation);
                enemyUnit.TakeDamage(1f);
                fx.transform.localScale = new Vector3(1, 1, 1);
            }
        }


        if (destroySelf && burstFX != null)
        {
            this.gameObject.SetActive(false);
            burstFX.transform.position = new Vector3(this.transform.position.x + burstOffset.x, this.transform.position.y + burstOffset.y, 0);
            burstFX.gameObject.SetActive(true);
            burstFX.Stop();
            burstFX.Play();
        }
    }

    void KnockbackPlayer(Collider2D collision)
    {
        if (!player.playerMovement.controller.Invulnerable && player.unit.currentHealth > 0)
        {
            player.unit.TakeDamage(damage);
            _camController.ShakeTheCamera();
            player.playerMovement.Stop();
            player.playerMovement.controller.MakeInvulnerable();
            player.playerMovement.controller.KnockbackCount = knockbackDuration;

            if (collision.transform.position.x < transform.position.x)
                player.playerMovement.controller.KnockFromRight = true;
            else
                player.playerMovement.controller.KnockFromRight = false;

        }
    }
}
