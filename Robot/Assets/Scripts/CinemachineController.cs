using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    public static CinemachineController Instance { get; private set; }

    private CinemachineVirtualCamera CinemachineVirtualCamera;
    private float shaketime;

    private void Awake()
    {
        Instance = this;
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>(); 
    }

    // Update is called once per frame
   public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin=
        CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shaketime = time;

        

    }

    private void Update()
    {
        if (shaketime > 0)
        {
            shaketime -= Time.deltaTime;
            if (shaketime <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
           
        }

    }


}
