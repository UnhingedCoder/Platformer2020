using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractableSpikeController : RetractSpike
{
    public float startDelay;
    float startTime = 0f;


    // Update is called once per frame
    void Update()
    {
        if (startTime <= startDelay)
        {
            startTime += Time.deltaTime;
        }
        else
        {
            if (upTime <= upDuration)
            {
                GrowSpike();
            }
            else
            {
                if (retractingTime <= retractingDuration)
                {
                    ShrinkSpike();
                }
                else
                {
                    upTime = 0;
                }
            }
        }
    }
}
