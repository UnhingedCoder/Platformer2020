using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpikeController : MonoBehaviour
{
    public float fallSpeed;
    public Rigidbody2D obstacleRb;
    public Vector2 initPos;
    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        initPos = obstacleRb.transform.localPosition;
        obstacleRb.gravityScale = 0;
    }

    public void DropTheSpike()
    {
        if(!player.playerMovement.controller.Invulnerable)
            obstacleRb.gravityScale = fallSpeed;
    }
}
