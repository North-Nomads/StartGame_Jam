using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using WorldGeneration;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Vector2 _moveInput;
        private int _playerMoves;
        [SerializeField] private WorldPlatform platform;
        [Tooltip("Default time between player actions"), SerializeField] private float defaultActionCooldown;
        // Action cooldown is a max cooldown time between actions that can be overwritten (boosted or slowed)
        private float _actionCooldown;
        // Timer variable that is updated in Update() function and counts time until it reaches _actionCooldown
        private float _currentActionCooldown; 
        
        private Vector2 _platformCoordinates;

        private Queue<Vector2> _playerPath = new Queue<Vector2>();
        
        public int PlayerPlatformX { get; set; }
        public int PlayerPlatformZ  { get; set; }

        /// <summary>
        /// A queue of coordinates which 
        /// </summary>
        public Queue<Vector2> PlayerPath => _playerPath;

        public WorldGenerator World { get; set; }

        /// <summary>
        /// Checks if player can move in certain direction (south, west, north or east)
        /// Called from InputSystem
        /// </summary>
        public void OnTryMovingSelfOnPlatform(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                // Get player input
                _moveInput = context.ReadValue<Vector2>();
                
                
                int moveX = DefineWorldSide(_moveInput.x);
                int moveZ = DefineWorldSide(_moveInput.y);

                if (moveX != 0 && moveZ != 0)
                    return;

                int targetX = PlayerPlatformX + moveX;
                int targetZ = PlayerPlatformZ + moveZ;
                
                if (targetX < 0 || targetX > World.LevelSizeX)
                    return;
                
                if (targetZ < 0 || targetZ > World.LevelSizeZ)
                    return;
                // Calculate target platform position
                var targetPlatform = World[targetX, targetZ];

                // Check if target platform is available to be stand on
                // Call MoveSelfOnPlatform(x, z) where x, z are indices of 2d array for target platform 
                if (targetPlatform.IsReachable)
                {
                    _playerPath.Enqueue(new Vector2(targetX, targetZ));
                    MoveSelfOnPlatform(targetX, targetZ);
                }
            }
            
            int DefineWorldSide(float input)
            {
                if (input == 0)
                    return 0;
                if (input > 0)
                    return 1;
                return -1;
            }
        }
        
        /// <summary>
        /// Moves self on new coordinates after all checks
        /// </summary>
        /// <param name="x">target platform x coordinate</param>
        /// <param name="z">target platform z coordinate</param>
        private void MoveSelfOnPlatform(int x, int z)
        {
            _playerMoves += 1;
            transform.position = World[x, z].PlayerPivot.position;
            PlayerPlatformX = x;
            PlayerPlatformZ = z;
            if (_playerMoves == 2)
            {

            }
        }
    }
}