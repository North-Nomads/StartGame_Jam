using System.Collections;
using Player;
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
            var queue = player.PlayerPath;
            if (queue.Count == 0)
                return;

            StartCoroutine(PerformSteppingBack());

            IEnumerator PerformSteppingBack()
            {
                for (int i = 0; i < backStepsAmount; i++)
                {
                    if (queue.Count == 0)
                        yield break;
                    
                    player.ReturnOneStepBack();
                    yield return new WaitForSeconds(timeBetweenSteps);
                }
            }
        }
    }
}