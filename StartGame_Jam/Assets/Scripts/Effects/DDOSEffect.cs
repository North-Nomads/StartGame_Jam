using System.Collections;
using Player;
using UnityEngine;
using WorldGeneration;

namespace Effects
{
    public class DDOSEffect : PlatformEffect
    {
        [SerializeField] private float effectDuration;
        
        public override void ExecuteOnPickUp(PlayerMovement player)
        {
            if (player.HasShield)
            {
                player.HandleBarrierBlock();
                return;
            }
            
            player.AreInputsReversed = true;
            StartCoroutine(WaitForDuration());

            IEnumerator WaitForDuration()
            {
                yield return new WaitForSeconds(effectDuration);
                player.AreInputsReversed = false;
            }
        }
    }
}