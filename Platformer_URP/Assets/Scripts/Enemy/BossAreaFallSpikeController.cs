using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaFallSpikeController : FallSpikeController
{
    public bool isPlayerUnder = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerUnder = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerUnder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerUnder = false;
        }
    }

    public void Reset()
    {
        obstacleRb.gravityScale = 0;
        obstacleRb.transform.localPosition = initPos;
        obstacleRb.gameObject.SetActive(true);
    }
}
