using Player;
using UnityEngine;

namespace WorldGeneration
{
    public abstract class PlatformEffect : MonoBehaviour  
    {
        public abstract void ExecuteOnPickUp(PlayerMovement player);
    }
}