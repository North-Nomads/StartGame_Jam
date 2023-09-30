using System.Collections;
using Player;
using UnityEngine;
using WorldGeneration;

namespace Effects
{
    public class EncryptionEffect : PlatformEffect
    {
        [SerializeField] private float newHackerAnimationTime;
        [SerializeField] private float boostTime;
        
        public override void ExecuteOnPickUp(PlayerMovement player)
        {
            if (player.HasBarrier)
            {
                player.HandleBarrierBlock();
                return;
            }

            player.Hacker.ActionTimer = newHackerAnimationTime;
            StartCoroutine(WaitForTime());

            IEnumerator WaitForTime()
            {
                yield return new WaitForSeconds(boostTime);
                player.Hacker.ActionTimer = player.Hacker.DefaultActionTimer;
            }
        }
    }
}