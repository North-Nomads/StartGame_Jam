using Player;
using System.Collections;
using UI;
using UnityEngine;

namespace WorldGeneration
{
    public class FishingTile : WorldPlatform
    {
        public override void OnReach(PlayerMovement player)
        {
            if (player.HasShield)
            {
                player.HandleBarrierBlock();
                return;
            }
            StartCoroutine(StunPlayer());
            IEnumerator StunPlayer()
            {
                PauseMenu.SetPlayerControls(false);
                yield return new WaitForSeconds(1);
                PauseMenu.SetPlayerControls(true);
            }
        }

        private void OnDestroy()
        {
            PauseMenu.SetPlayerControls(true);
        }
    }
}