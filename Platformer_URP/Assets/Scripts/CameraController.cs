using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public float shakeTime;
    public float amplitudeGain;
    public float frequencyGain;
    float timer = 0f;
    bool shakeCamera = false;
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
            Noise(amplitudeGain, frequencyGain);
            timer += Time.deltaTime;
            if (timer > shakeTime)
            {
                shakeCamera = false;
                timer = 0;
                Noise(0f, 0f);
            }
        }
    }

    public void Noise(float _amplitudeGain, float _frequencyGain)
    {
        cinevCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _amplitudeGain;
        cinevCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = _frequencyGain;
    }

    public void ShakeTheCamera()
    {
        shakeCamera = true;
    }
}
