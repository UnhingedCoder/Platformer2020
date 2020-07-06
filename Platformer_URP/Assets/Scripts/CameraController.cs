using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public float shakeTime;

    float timer = 0f;
    public bool shakeCamera = false;
    CinemachineVirtualCamera cinevCam;

    private void Awake()
    {
        cinevCam = GetComponent<CinemachineVirtualCamera>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeCamera)
        {
            Noise(1f, 1f);
            timer += Time.deltaTime;
            if (timer > shakeTime)
            {
                shakeCamera = false;
                timer = 0;
                Noise(0f, 0f);
            }
        }
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        cinevCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeGain;
        cinevCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequencyGain;
    }

    public void ShakeTheCamera()
    {
        shakeCamera = true;
    }
}
