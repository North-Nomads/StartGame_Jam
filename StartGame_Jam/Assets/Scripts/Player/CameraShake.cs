using Cinemachine;
using Player;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float _shakeTimer;
    // private float shakeTime;
    // private float amplitudeGain;
    // private float frequencyGain;


    public CinemachineVirtualCamera _cinemachineVirtualCamera { get; set; }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 2;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 7;
        _shakeTimer = 0.3f;
    }


    private void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
                cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
            }
        }
    }
}