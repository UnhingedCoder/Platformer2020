using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionFallSpikeController : FallSpikeController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DropTheSpike();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DropTheSpike();
        }
    }
}
