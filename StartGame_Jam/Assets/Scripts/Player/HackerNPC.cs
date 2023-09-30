using UnityEngine;

namespace Player
{
    public class HackerNPC : MonoBehaviour
    {
        [SerializeField] private PlayerMovement targetPlayer;
        
        [Tooltip("Default time between player actions"), SerializeField] private float defaultActionCooldown;
        // Action cooldown is a max cooldown time between actions that can be overwritten (boosted or slowed)
        private float _actionCooldown; 
        // Timer variable that is updated in Update() function and counts time until it reaches _actionCooldown
        private float _currentActionCooldown;

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