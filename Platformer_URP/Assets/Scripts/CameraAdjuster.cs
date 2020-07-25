using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    private CameraController cam;
    public bool increaseFOV;

    private void Awake()
    {
        cam = FindObjectOfType<CameraController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (increaseFOV)
                cam.IncreaseFOVAdjustment();
            else
                cam.DecreaseFOVAdjustment();
        }
    }
}
