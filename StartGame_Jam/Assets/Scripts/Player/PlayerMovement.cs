using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WorldGeneration;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Tooltip("Default time between player actions"), SerializeField] private float defaultActionCooldown;
        [SerializeField] private float hackerDelay;
        [SerializeField] private HackerNPC hacker;

        private float _currentActionCooldown; // Movement timer (updating in Update())
        private readonly Queue<Vector2Int> _playerPath = new();
        private Vector2 _moveInput;
        private int _playerMoves;
        
        /// <summary>
        /// Current X of platform on which player stands
        /// </summary>
        public int PlayerPlatformX { get; set; }
        
        /// <summary>
        /// Current Z of platform on which player stands
        /// </summary>
        public int PlayerPlatformZ  { get; set; }

        /// <summary>
        /// A queue of coordinates which 
        /// </summary>
        public Queue<Vector2Int> PlayerPath => _playerPath;
        
        /// <summary>
        /// World object that has all information about the platforms
        /// </summary>
        public WorldGenerator World { get; set; }

        /// <summary>
        /// Current max timer value
        /// </summary>
        public float ActionCooldown { get; set; }
        
        /// <summary>
        /// Hacker that chases the player
        /// </summary>
        public HackerNPC Hacker { get; set; }

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
                    _playerPath.Enqueue(new Vector2Int(targetX, targetZ));
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
            transform.position = World[x, z].PlayerPivot.position;
            PlayerPlatformX = x;
            PlayerPlatformZ = z;

            if (Hacker is not null)
                return;
            
            Hacker = Instantiate(hacker);
            Hacker.CallOnHackerSpawn(_playerPath.Peek(), World, this);
        }
    }
}