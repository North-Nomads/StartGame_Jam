using System.Collections;
using Player;
using UnityEngine;
using WorldGeneration;

namespace Effects
{
    public class MinerEffect : PlatformEffect
    {
        [SerializeField] private float effectDuration;
        [SerializeField] private float fullZoomTime;
        [SerializeField] private float targetCameraOrthoSize;
        private float _defaultCameraOrthoSize;

        public override void ExecuteOnPickUp(PlayerMovement player)
        {
            if (player.HasShield)
            {
                player.HandleBarrierBlock();
                return;
            }
            
            var virtualCamera = player.CameraShake.VirtualCamera;
            _defaultCameraOrthoSize = virtualCamera.m_Lens.OrthographicSize;

            StartCoroutine(ZoomInDuringTime());
            
            IEnumerator ZoomInDuringTime()
            {
                var difference = targetCameraOrthoSize - _defaultCameraOrthoSize;
                var delta = difference / fullZoomTime;
                var time = 0f;

                while (time < fullZoomTime)
                {
                    virtualCamera.m_Lens.OrthographicSize += delta;
                    time += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}