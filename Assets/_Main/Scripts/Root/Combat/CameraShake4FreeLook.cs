using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake4FreeLook : MonoBehaviour
{
    public static CameraShake4FreeLook Instance { get; private set; }
    CinemachineFreeLook cinemachine;
    float shakerTimer;
    float shakerTimerTotal;
    float startingIntensity;
    void Awake()
    {
        Instance = this;
        cinemachine = GetComponent<CinemachineFreeLook>();
 
    }
 
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin[] cinemachineBasicMultiChannelPerlins = cinemachine.GetComponentsInChildren<CinemachineBasicMultiChannelPerlin>();
        foreach (var cinemachineBasicMultiChannelPerlin in cinemachineBasicMultiChannelPerlins)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        }
        startingIntensity = intensity;
        shakerTimerTotal = time;
        shakerTimer = time;
    }
 
    private void Update()
    {
        if (shakerTimer > 0)
        {
            shakerTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin[] cinemachineBasicMultiChannelPerlins = cinemachine.GetComponentsInChildren<CinemachineBasicMultiChannelPerlin>();
            foreach (var cinemachineBasicMultiChannelPerlin in cinemachineBasicMultiChannelPerlins)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1f - shakerTimer / shakerTimerTotal);
            }
        }
    }
}