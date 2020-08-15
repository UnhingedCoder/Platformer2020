using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractSpike : MonoBehaviour
{
    public float upDuration;
    public float upTime;


    public float retractingDuration;
    public float retractingTime;

    public Transform spike;

    private BoxCollider2D collisionBox;

    public bool ShouldGrow;

    // Start is called before the first frame update
    void Start()
    {
        spike.localScale = Vector3.zero;
        collisionBox = spike.GetComponent<BoxCollider2D>();
    }

    public void ShrinkSpike()
    {
        retractingTime += Time.deltaTime;

        spike.localScale = Vector3.Lerp(spike.localScale, Vector3.zero, retractingTime);

        if (spike.localScale.x < 0.5)
            collisionBox.enabled = false;
    }

    public void GrowSpike()
    {
        upTime += Time.deltaTime;
        retractingTime = 0;
        spike.localScale = Vector3.Lerp(spike.localScale, new Vector3(1, 1, 1), upTime);

        if (spike.localScale.x > 0.5)
            collisionBox.enabled = true;
    }
}
