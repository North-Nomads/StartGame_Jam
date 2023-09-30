using UnityEngine;
using WorldGeneration;

namespace Player
{
    public class HackerNPC : MonoBehaviour
    {
        [SerializeField] private PlayerMovement targetPlayer;
        
        [Tooltip("Default time between player actions"), SerializeField] private float defaultActionCooldown;
        private bool IsdefaultActionCooldown => _currentActionCooldown > 0;
        // Action cooldown is a max cooldown time between actions that can be overwritten (boosted or slowed)
        private float _actionCooldown;
        // Timer variable that is updated in Update() function and counts time until it reaches _actionCooldown
        private float _currentActionCooldown;

        public WorldGenerator World { get; set; }
        private void Start()
        {
            _currentActionCooldown = defaultActionCooldown;
        }
        private void Update()
        {
            if (IsdefaultActionCooldown)
            {
                defaultActionCooldown -= Time.deltaTime;
            }
            else 
            {
                MoveOnNextPlatform();
                _currentActionCooldown = defaultActionCooldown;
            }

        }

        private void MoveOnNextPlatform()
        {
            
            // Get player path Queue 
            // Move On it
            // Check if player was caught (game finishes)
            // Reset cooldown
        }

        /// <summary>
        /// Checks if hacker is on the same platform as player
        /// </summary>
        /// <returns></returns>
        private bool HasReachedPlayer()
        {
            return false;
        }
    }
}