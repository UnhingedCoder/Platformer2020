using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitController : Unit
{
    public void TakeDamage(float dmg)
    {
        if ((currentHealth - dmg) > 0)
        {
            ps_damageTaken.gameObject.SetActive(true);
            ps_damageTaken.Stop();
            ps_damageTaken.Play();

            currentHealth -= dmg;
        }
        else
        {
            currentHealth = 0;
            this.gameObject.SetActive(false);
        }
    }

    public void Heal(float health)
    {
        if ((currentHealth + health) > totalHealth)
            currentHealth = totalHealth;
        else
            currentHealth += health;
    }
}
