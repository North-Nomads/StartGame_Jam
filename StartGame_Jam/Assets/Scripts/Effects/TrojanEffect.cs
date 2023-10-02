using System.Collections;
using Player;
using UnityEngine;
using WorldGeneration;

namespace Effects
{
    public class TrojanEffect : PlatformEffect
    {
        [SerializeField] private float effectDuration;
        [SerializeField] private float effectPower;

        private PlayerMovement _player;
        
        public override void ExecuteOnPickUp(PlayerMovement player)
        {
            if (player.HasShield)
            {
                player.HandleBarrierBlock();
                return;
            }

            print(player.ActionCooldown);
            player.ActionCooldown /= effectPower;
            print(player.ActionCooldown);

            StartCoroutine(DurationCoroutine());

            IEnumerator DurationCoroutine()
            {
                yield return new WaitForSeconds(effectDuration);
                player.ActionCooldown *= effectPower;
            }
        }
    }
}