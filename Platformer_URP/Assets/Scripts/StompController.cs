using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StompController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    public UnityEvent e_OnStomp;

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
        if (collision.CompareTag("EnemyHead"))
        {
            if (!_playerMovement.controller.Invulnerable)
            {
                Destroy(collision.transform.parent.gameObject);
                e_OnStomp.Invoke();
            }
        }
    }
}
