using System.Collections;
using Player;
using UnityEngine;
using WorldGeneration;

namespace Effects
{
    public class CatVideoEffect : PlatformEffect
    {
        [SerializeField] private float effectDuration;
        [SerializeField] private float effectPower;

        private PlayerMovement _player;

        public override void ExecuteOnPickUp(PlayerMovement player)
        {
            player.ActionCooldown /= effectPower;

            StartCoroutine(DurationCoroutine());

            IEnumerator DurationCoroutine()
            {
                yield return new WaitForSeconds(effectDuration);
                player.ActionCooldown *= effectPower;
            }
        }
    }
}