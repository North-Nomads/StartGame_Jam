using UnityEngine;
using WorldGeneration;

namespace Player
{
    public class HackerNPC : MonoBehaviour
    {
        [SerializeField] private float defaultActionTimer;
        private float _currentActionTimer;
        private Vector2Int _hackerPosition;
        private float _startDelay;

        public PlayerMovement TargetPlayer { get; set; }
        public WorldGenerator World { get; set; }
        public float ActionTimer { get; set; }

        private void Update()
        {
            if (_startDelay > 0)
            {
                _startDelay -= Time.deltaTime;
                return;
            }
            
            if (_currentActionTimer > 0)
            {
                _currentActionTimer -= Time.deltaTime;
                return;
            }

            MoveOnNextPlatform();
            _currentActionTimer = ActionTimer;
        }

        private void MoveOnNextPlatform()
        {
            // DEBUG ONLY 
            // Disable error throwing on player caught
            if (HasReachedPlayer())
                return;
            
            var targetPlatform = TargetPlayer.PlayerPath.Dequeue();
            transform.position = World[targetPlatform.x, targetPlatform.y].PlayerPivot.position;
            _hackerPosition = targetPlatform;
            
            if (HasReachedPlayer())
                World.HandlePlayerLose();
        }

        /// <summary>
        /// Checks if hacker is on the same platform as player
        /// </summary>
        /// <returns>Is player platform == hacker platform</returns>
        public bool HasReachedPlayer()
        {
            var playerPosition = new Vector2Int(TargetPlayer.PlayerPlatformX, TargetPlayer.PlayerPlatformZ);
            return playerPosition == _hackerPosition;
        }

        public void CallOnHackerSpawn(WorldGenerator world, PlayerMovement playerMovement, float hackerDelay)
        {
            TargetPlayer = playerMovement;
            World = world;
            
            transform.position = World[0, 0].PlayerPivot.position;
            _hackerPosition = Vector2Int.zero;
            ActionTimer = defaultActionTimer;
            _currentActionTimer = ActionTimer;
            _startDelay = hackerDelay;
        }
    }
}