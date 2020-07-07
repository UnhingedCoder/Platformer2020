using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackController : MonoBehaviour
{
    public float knockbackDuration;
    public float knockbackPower;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();    
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
            if (!_playerMovement.controller.Invulnerable)
            {
                _playerMovement.Stop();
                _playerMovement.controller.MakeInvulnerable();
                _playerMovement.controller.KnockbackCount = knockbackDuration;

                if (collision.transform.position.x < transform.position.x)
                    _playerMovement.controller.KnockFromRight = true;
                else
                    _playerMovement.controller.KnockFromRight = false;
            }
        }
    }
}
