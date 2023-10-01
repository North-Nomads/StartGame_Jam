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
        [SerializeField] private float zoomOutTime;
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
                    print($"{delta * Time.deltaTime}, {virtualCamera.m_Lens.OrthographicSize}, {fullZoomTime}/{time}");
                    virtualCamera.m_Lens.OrthographicSize += delta * Time.deltaTime;
                    time += Time.deltaTime;
                    print($"Time {time}");
                    yield return null;
                }

                print("Waiting...");
                yield return new WaitForSeconds(effectDuration - fullZoomTime - zoomOutTime);

                print("Continue");
                difference = _defaultCameraOrthoSize - targetCameraOrthoSize;
                delta = difference / zoomOutTime;
                time = 0f;

                print("Zooming out");
                while (time < zoomOutTime)
                {
                    print(virtualCamera.m_Lens.OrthographicSize);
                    virtualCamera.m_Lens.OrthographicSize += delta * Time.deltaTime;
                    time += Time.deltaTime;
                    yield return null;
                }
                print("Finish");
            }
        }
    }
}