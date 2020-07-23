using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitController : Unit
{
    public bool invulnerable;
    public float invulnerabliltyDuration;
    private float invulnerabilityTime;

    public string objectPoolerName;
    public GameObject orb;
    private ObjectPooler _objectPooler;
    private Animator anim;

    private void Awake()
    {
        _objectPooler = GameObject.Find("ObjectPoolers/" + objectPoolerName).GetComponent<ObjectPooler>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (invulnerabilityTime > 0)
        {
            invulnerabilityTime -= Time.deltaTime;
            invulnerable = true;
        }
        else
        {
            invulnerable = false;
            if (anim)
                anim.SetTrigger("Normal");
        }
    }

    public void TakeDamage(float dmg)
    {
        if (invulnerable)
            return;

        if ((currentHealth - dmg) > 0)
        {
            ps_damageTaken.gameObject.SetActive(true);
            ps_damageTaken.Stop();
            ps_damageTaken.Play();

            currentHealth -= dmg;
            invulnerabilityTime = invulnerabliltyDuration;
            invulnerable = true;
            if (anim)
                anim.SetTrigger("Invulnerable");

        }
        else
        {
            currentHealth = 0;
            //Vector2 spawnPos = new Vector2(this.transform.position);
            orb = _objectPooler.GetPooledObject(this.transform.position);
            orb.SetActive(true);

            this.gameObject.SetActive(false);

        }
    }

    public void Heal(float health)
    {
        if (currentHealth >= totalHealth)
            return;

        if ((currentHealth + health) > totalHealth)
            currentHealth = totalHealth;
        else
            currentHealth += health;
    }
}
