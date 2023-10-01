using System;
using Cinemachine;
using UnityEngine;

namespace Camera
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float wallBumpAmplitude;
        [SerializeField] private float wallBumpFrequency;
        [SerializeField] private float wallBumpShakeTimer;

        private float _timeLeft;
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _perlin;

        public CinemachineVirtualCamera VirtualCamera => _virtualCamera;

        private void Start()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        
        public void ShakeCameraOnBump()
        {
            _perlin.m_AmplitudeGain = wallBumpAmplitude;
            _perlin.m_FrequencyGain = wallBumpFrequency;
            _timeLeft = wallBumpShakeTimer;
        }

        public void StopCameraShake()
        {
            _perlin.m_AmplitudeGain = 0;
            _perlin.m_FrequencyGain = 0;
        }
        
        private void Update()
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                return;
            }
            
            StopCameraShake();
        }
    }
}