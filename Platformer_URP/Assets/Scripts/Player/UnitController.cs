using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitController : MonoBehaviour
{
    public float totalHealth = 3;
    public float currentHealth;
    public ParticleSystem ps_damageTaken;

    public UnityEvent e_HealthChanged;
    public UnityEvent e_HealRegained;
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

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

    public bool IsAlive()
    {
        return currentHealth > 0 ? true : false;
    }

}
