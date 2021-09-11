using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineVirtualCamera vcm;
    private CinemachineBasicMultiChannelPerlin shakeMeter;
    private float shakeTime;
    private float startIntensity;
    private float TimeTotal;

    private void Awake()
    {
        Instance = this;
        vcm = GetComponent<CinemachineVirtualCamera>();
        shakeMeter = vcm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeThat(float intensity, float time)
    {
        shakeMeter.m_AmplitudeGain = intensity;
        startIntensity = intensity;
        shakeTime = time;
    }

    private void Update()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            shakeMeter.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, 1 - (shakeTime / TimeTotal));
        }
    }
}

