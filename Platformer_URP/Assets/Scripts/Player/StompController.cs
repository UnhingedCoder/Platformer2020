using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StompController : MonoBehaviour
{
    private PlayerController player;
    public UnityEvent e_OnStomp;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyHead"))
        {
            Debug.Log("Stomped on enemy head");
            if (!player.playerMovement.controller.Invulnerable)
            {
                DamageEnemy(collision);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyHead"))
        {
            Debug.Log("Stomped on enemy head");
            if (!player.playerMovement.controller.Invulnerable)
            {
                DamageEnemy(collision);
            }
        }
    }

    void DamageEnemy(Collider2D collision)
    {
        player.camController.ShakeTheCamera();
        player.playerMovement.dir = 0;
        EnemyUnitController enemyUnit = collision.transform.parent.GetComponent<EnemyUnitController>();
        GameObject fx = Instantiate(enemyUnit.ps_damageTaken.gameObject, new Vector3(this.transform.position.x, this.transform.position.y, 0), enemyUnit.ps_damageTaken.transform.rotation);
        enemyUnit.TakeDamage(1f);
        fx.transform.localScale = new Vector3(1, 1, 1);
        e_OnStomp.Invoke();
    }
}
