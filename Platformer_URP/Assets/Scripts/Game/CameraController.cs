using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public float shakeTime;
    public float amplitudeGain;
    public float frequencyGain;
    float shakeTimer = 0f;
    bool shakeCamera = false;

    public int maxFOV;
    public int minFOV;
    float fovTimer;
    public bool increaseFOV = false;
    public bool decreaseFOV = false;
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
            shakeTimer += Time.deltaTime;
            if (shakeTimer > shakeTime)
            {
                shakeCamera = false;
                shakeTimer = 0;
                Noise(0f, 0f);
            }
        }

        if (increaseFOV)
        {
            FOVAdjustment(maxFOV);
            fovTimer += (0.5f * Time.deltaTime);
            if (cinevCam.m_Lens.OrthographicSize >= maxFOV)
            {
                increaseFOV = false;
                fovTimer = 0;
            }
        }
        else if (decreaseFOV)
        {
            FOVAdjustment(minFOV);
            fovTimer += (0.5f * Time.deltaTime);
            if (cinevCam.m_Lens.OrthographicSize <= minFOV)
            {
                decreaseFOV = false;
                fovTimer = 0;
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

    public void FOVAdjustment(int targetFOV)
    {
        cinevCam.m_Lens.OrthographicSize = Mathf.Lerp(cinevCam.m_Lens.OrthographicSize, targetFOV, fovTimer);
    }

    public void IncreaseFOVAdjustment()
    {
        decreaseFOV = false;
        increaseFOV = true;
    }

    public void DecreaseFOVAdjustment()
    {
        decreaseFOV = true;
        increaseFOV = false;
    }
}
