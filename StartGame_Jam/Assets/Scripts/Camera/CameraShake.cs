using Cinemachine;
using UnityEngine;

namespace Camera
{
    public class CameraShake : MonoBehaviour
    {
        private float _shakeTimer;
        
        public CinemachineVirtualCamera CinemachineVirtualCamera { get; set; }

        public void ShakeCamera()
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
                    cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
                }
            }
        }
    }
}