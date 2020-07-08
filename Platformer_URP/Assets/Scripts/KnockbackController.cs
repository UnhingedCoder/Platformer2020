using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackController : MonoBehaviour
{
    public float damage;
    public float knockbackDuration;

    public GameObject burstFX;
    private UnitController _unitController;
    private PlayerMovement _playerMovement;
    private CameraController _camController;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _camController = FindObjectOfType<CameraController>();
        _unitController = _playerMovement.GetComponent<UnitController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KnockbackPlayer(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KnockbackPlayer(collision);
        }
    }

    void KnockbackPlayer(Collider2D collision)
    {
        if (!_playerMovement.controller.Invulnerable && _unitController.currentHealth > 0)
        {
            _unitController.TakeDamage(damage);
            _camController.ShakeTheCamera();
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
