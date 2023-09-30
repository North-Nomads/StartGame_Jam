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
            // player.hacker.actiontimer = newHackerAnimationTime
            StartCoroutine(WaitForTime());

            IEnumerator WaitForTime()
            {
                yield return new WaitForSeconds(boostTime);
            }
        }
    }
}