using System;
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
        [SerializeField] private bool isCheckPoint;

        /// <summary>
        /// A transform that determines position on a player.
        /// </summary>
        public Transform PlayerPivot => playerPivot;

        /// <summary>
        /// Defines if player can stand on this platform or not.
        /// </summary>
        public bool IsReachable => isReachable;

        /// <summary>
        /// Determines whether the platform is a checkpoint.
        /// </summary>
        public bool IsCheckPoint => isCheckPoint;

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

        public void DisableEffect()
        {
            Effect.IsPickable = false;
            Effect.HideChildren();
        }
    }

    /// <summary>
    /// Defines the flags to serialize the platforms into a file.
    /// </summary>
    [Serializable]
    [Flags]
    public enum PlatformFlags : byte
    {
        /// <summary>
        /// There are not any flags attached to the platform.
        /// </summary>
        None = 0,
        /// <summary>
        /// The platform is rotated by 90 degrees over the Y axis.
        /// </summary>
        RotateBy90,
        /// <summary>
        /// The platform is rotated by 180 degrees over the Y axis.
        /// </summary>
        RotateBy180
    }
}
