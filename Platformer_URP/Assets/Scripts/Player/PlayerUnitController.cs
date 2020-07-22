using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUnitController : Unit
{
    public ParticleSystem ps_damageTaken;

    public UnityEvent e_HealthChanged;
    public UnityEvent e_HealRegained;


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
        }

        e_HealthChanged.Invoke();
    }

    public void Heal(float health)
    {
        if ((currentHealth + health) > totalHealth)
            currentHealth = totalHealth;
        else
            currentHealth += health;

        e_HealthChanged.Invoke();
        e_HealRegained.Invoke();
    }

}
