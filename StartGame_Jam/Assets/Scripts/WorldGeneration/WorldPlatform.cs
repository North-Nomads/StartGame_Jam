using UnityEngine;

namespace WorldGeneration
{
    /// <summary>
    /// Platforms that build entire level space
    /// </summary>
    public class WorldPlatform : MonoBehaviour
    {
        [SerializeField] private Transform playerPivot;
        [SerializeField] private bool isReachable;

        /// <summary>
        /// A transform that determines position on a player.
        /// </summary>
        public Transform PlayerPivot => playerPivot;

        /// <summary>
        /// Defines if player can stand on this platform or not.
        /// </summary>
        public bool IsReachable => isReachable;

        /// <summary>
        /// The X coordinate of platform in 2DArray of all generator platforms.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Z coordinate of platform in 2DArray of all generator platforms.
        /// </summary>
        public int Z { get; set; }

        /// <summary>
        /// Effect that is applied when player stands on the platform
        /// </summary>
        public PlatformEffect Effect { get; set; }
    }
}
