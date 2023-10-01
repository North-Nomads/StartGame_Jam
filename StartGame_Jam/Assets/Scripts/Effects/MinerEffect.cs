using System.Collections;
using Player;
using UnityEngine;
using WorldGeneration;

namespace Effects
{
    public class MinerEffect : PlatformEffect
    {
        [SerializeField] private float effectDuration;
        [SerializeField] private float targetCameraZoomSize;

        public override void ExecuteOnPickUp(PlayerMovement player)
        {
            if (player.HasShield)
            {
                player.HandleBarrierBlock();
                return;
            }

            /*var mainCamera = Camera.main;
            var cameraStartSize = mainCamera.orthographicSize;
            mainCamera.orthographicSize = targetCameraZoomSize;
            StartCoroutine(WaitForSeconds());

            IEnumerator WaitForSeconds()
            {
                yield return new WaitForSeconds(effectDuration);
                mainCamera.orthographicSize = cameraStartSize;

            }*/
        }
    }
}