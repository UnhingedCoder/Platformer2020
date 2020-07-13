using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionObstacleController : MonoBehaviour
{
    public float fallSpeed;
    public Rigidbody2D obstacleRb;

    // Start is called before the first frame update
    void Start()
    {
        obstacleRb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            obstacleRb.gravityScale = fallSpeed;
        }
    }
}
