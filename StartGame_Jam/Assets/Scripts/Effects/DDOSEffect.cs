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
            // set player inversed bool -> True 
            StartCoroutine(WaitForDuration());

            IEnumerator WaitForDuration()
            {
                yield return new WaitForSeconds(effectDuration);
                // set player inversed bool -> False
            }
        }
    }
}