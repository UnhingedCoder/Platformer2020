using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionObstacleController : MonoBehaviour
{
    public float fallSpeed;
    public Rigidbody2D obstacleRb;
    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        obstacleRb.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !player.playerMovement.controller.Invulnerable)
        {
            obstacleRb.gravityScale = fallSpeed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !player.playerMovement.controller.Invulnerable)
        {
            obstacleRb.gravityScale = fallSpeed;
        }
    }
}
