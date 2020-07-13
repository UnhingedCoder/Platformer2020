using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpController : MonoBehaviour
{
    public float HealAmount;

    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.unit.currentHealth < player.unit.totalHealth)
            {
                player.unit.Heal(HealAmount);
                this.gameObject.SetActive(false);
            }
        }
    }
}
