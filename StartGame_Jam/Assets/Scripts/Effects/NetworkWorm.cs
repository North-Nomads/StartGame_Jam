using System.Collections;
using Player;
using UI;
using UnityEngine;
using WorldGeneration;

namespace Effects
{
    public class NetworkWorm : PlatformEffect
    {
        [SerializeField] private int backStepsAmount;
        [SerializeField] private float timeBetweenSteps;
        
        public override void ExecuteOnPickUp(PlayerMovement player)
        {
            if (player.HasShield)
            {
                player.HandleBarrierBlock();
                return;
            }

            var playerPath = player.PlayerPath;
            if (playerPath.Count == 0)
                return;

            PauseMenu.SetPlayerControls(false);
            StartCoroutine(PerformSteppingBack());

            IEnumerator PerformSteppingBack()
            {
                player.RemoveLastStep();
                yield return new WaitForSeconds(timeBetweenSteps);

                for (int i = 0; i < backStepsAmount; i++)
                {
                    if (playerPath.Count == 0)
                        yield break;
                    
                    player.GoOneStepBack();
                    yield return new WaitForSeconds(timeBetweenSteps);
                }
                
                PauseMenu.SetPlayerControls(true);
            }
        }

        private void OnDestroy()
        {
            PauseMenu.SetPlayerControls(true);
        }
    }
}