using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public float totalHealth;
    public float currentHealth;

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
        currentHealth -= dmg;
    }

    public bool IsAlive()
    {
        return currentHealth > 0 ? true : false;
    }

}
