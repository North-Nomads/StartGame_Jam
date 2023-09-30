using UnityEngine;

namespace WorldGeneration
{
    /// <summary>
    /// Platforms that build entire level space
    /// </summary>
    public class WorldPlatform : MonoBehaviour
    {
        [SerializeField] private Transform playerPivot;
        private PlatformEffect _effect;
        private bool _isReachable;
        private int _x;
        private int _z;
        
        /// <summary>
        /// A transform that determines position on a player
        /// </summary>
        public Transform PlayerPivot => playerPivot;
        
        /// <summary>
        /// Defines if player can stand on this platform or not
        /// </summary>
        public bool IsReachable => _isReachable;
        
        /// <summary>
        /// The X coordinate of platform in 2DArray of all generator platforms   
        /// </summary>
        public int X => _x;
        
        /// <summary>
        /// The Z coordinate of platform in 2DArray of all generator platforms   
        /// </summary>
        public int Z => _z;

        /// <summary>
        /// Effect that is applied when player stands on the platform
        /// </summary>
        public PlatformEffect Effect => _effect;
    }
}
