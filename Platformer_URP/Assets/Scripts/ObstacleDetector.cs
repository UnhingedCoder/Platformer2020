using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleDetector : MonoBehaviour
{
    [SerializeField] UnityEvent e_HitAObstacle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log(collision.gameObject.name + " in front");
            e_HitAObstacle.Invoke();
        }
    }
}
