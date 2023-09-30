using System.Collections;
using Player;
using UnityEngine;
using WorldGeneration;

namespace Effects
{
    public class FriendsEffect : PlatformEffect
    {
        [SerializeField] private float effectDuration;
        [SerializeField] private int barrierRadius;
        public override void ExecuteOnPickUp(PlayerMovement player)
        {
            player.BarrierRadius = barrierRadius;
            StartCoroutine(WaitForDuration());

            IEnumerator WaitForDuration()
            {
                yield return new WaitForSeconds(effectDuration);
                // player.hacker.BarrierRadius = 0; 
            }
        }
    }
}