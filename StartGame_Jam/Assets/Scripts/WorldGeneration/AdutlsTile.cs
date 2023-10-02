using Player;
using System.Collections;
using UnityEngine;

namespace WorldGeneration
{
    public class AdultsTile : WorldPlatform
    {
        public override void OnReach(PlayerMovement player)
        {
            StartCoroutine(PlayerBack());

            IEnumerator PlayerBack()
            {
                if (player.PlayerPath.Count <= 1)
                    yield break;
                player.RemoveLastStep();
                yield return new WaitForSeconds(0.3f);
                player.GoOneStepBack();
            }
           
        }
    }
}