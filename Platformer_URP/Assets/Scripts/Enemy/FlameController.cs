using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    public float startDelay;
    float startTime = 0f;

    public float flameDuration;
    float flameTime;
    public float noFlameDuration;
    float noFlameTime;


    public ParticleSystem myParticleSystem;

    BoxCollider2D coll;
    ParticleSystem.EmissionModule emissionModule;

    private void Awake()
    {
        emissionModule = myParticleSystem.emission;
        coll = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
            if (flameTime <= flameDuration)
            {
                flameTime += Time.deltaTime;
                noFlameTime = 0;
                emissionModule.rateOverTime = 80f;
                coll.enabled = true;
            }
            else
            {
                if (noFlameTime <= noFlameDuration)
                {
                    noFlameTime += Time.deltaTime;

                    emissionModule.rateOverTime = 0f;
                    coll.enabled = false;
                }
                else
                {
                    flameTime = 0;
                }
            }
        }
    }
}
