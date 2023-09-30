using Player;
using UnityEngine;
using WorldGeneration;

namespace Effects
{
    public class NetworkWorm : MonoBehaviour, IPlatformEffect
    {
        [SerializeField] private int reverseSteps;
        
        public void ExecuteOnPickUp(PlayerMovement player)
        {
            //
        }
    }
}