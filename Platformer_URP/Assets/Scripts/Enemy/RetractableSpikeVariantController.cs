using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractableSpikeVariantController : RetractSpike
{
    public int id;

    void Update()
    {
        if(ShouldGrow)
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
                    ShouldGrow = false;
                }
            }
        }
    }
}
