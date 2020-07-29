using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractableSpikeController : MonoBehaviour
{
    public float startDelay;
    float startTime = 0f;

    public float upDuration;
    float upTime;


    public float retractingDuration;
    float retractingTime;

    public Transform spike;
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        spike.localScale = Vector3.zero; 
    }

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
                upTime += Time.deltaTime;
                retractingTime = 0;
                spike.localScale = Vector3.Lerp(spike.localScale, new Vector3(1, 1, 1), upTime);            }
            else
            {
                if (retractingTime <= retractingDuration)
                {
                    retractingTime += Time.deltaTime;

                    spike.localScale = Vector3.Lerp(spike.localScale, Vector3.zero, retractingTime);
                }
                else
                {
                    upTime = 0;
                }
            }
        }
    }
}
