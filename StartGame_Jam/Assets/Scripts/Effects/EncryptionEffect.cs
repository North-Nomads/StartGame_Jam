using System.Collections;
using Player;
using UnityEngine;
using WorldGeneration;

namespace Effects
{
    public class EncryptionEffect : PlatformEffect
    {
        [SerializeField] private float boostPower;
        [SerializeField] private float boostTime;
        
        public override void ExecuteOnPickUp(PlayerMovement player)
        {
            if (player.HasShield)
            {
                player.HandleBarrierBlock();
                return;
            }

            player.Hacker.ActionTimer *= boostPower;
            StartCoroutine(WaitForTime());

            IEnumerator WaitForTime()
            {
                yield return new WaitForSeconds(boostTime);
                player.Hacker.ActionTimer /= boostPower;
            }
        }
    }
}