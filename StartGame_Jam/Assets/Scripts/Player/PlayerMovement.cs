using System.Collections.Generic;
using UnityEngine;
using WorldGeneration;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {   
        [Tooltip("Default time between player actions"), SerializeField] private float defaultActionCooldown;
        // Action cooldown is a max cooldown time between actions that can be overwritten (boosted or slowed)
        private float _actionCooldown;
        // Timer variable that is updated in Update() function and counts time until it reaches _actionCooldown
        private float _currentActionCooldown; 
        
        private Vector2 _platformCoordinates;

        private Queue<Vector2> _playerPath;

        /// <summary>
        /// A queue of coordinates which 
        /// </summary>
        public Queue<Vector2> PlayerPath => _playerPath;

        /// <summary>
        /// Current coordinate of the platform on which player stands  
        /// </summary>
        public Vector2 PlatformCoordinates => _platformCoordinates;

        public WorldGenerator World { get; set; }

        /// <summary>
        /// Checks if player can move in certain direction (south, west, north or east)
        /// Called from InputSystem
        /// </summary>
        private void TryMovingSelfOnPlatform()
        {
            // Get player input
            // Transform input into Vector2 
            // Calculate target platform position
            // Check if target platform is available to be stand on
            // Call MoveSelfOnPlatform(x, z) where x, z are indices of 2d array for target platform 
        }
        
        /// <summary>
        /// Moves self on new coordinates after all checks
        /// </summary>
        /// <param name="x">target platform x coordinate</param>
        /// <param name="z">target platform z coordinate</param>
        private void MoveSelfOnPlatform(int x, int z)
        {
            
        }
    }
}