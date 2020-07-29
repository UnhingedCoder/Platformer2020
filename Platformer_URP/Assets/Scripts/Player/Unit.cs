using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public ParticleSystem ps_damageTaken;
    public ParticleSystem ps_HealUp;

    public float totalHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0 ? true : false;
    }
}
