using UnityEngine;
using WorldGeneration;

namespace Player
{
    public class HackerNPC : MonoBehaviour
    {
        [SerializeField] private float defaultActionTimer;
        private float _currentActionTimer;
        private Vector2Int _hackerPosition;
        
        public PlayerMovement TargetPlayer { get; set; }
        public WorldGenerator World { get; set; }
        public float ActionTimer { get; set; }

        private void Update()
        {
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
        private bool HasReachedPlayer()
        {
            var playerPosition = new Vector2Int(TargetPlayer.PlayerPlatformX, TargetPlayer.PlayerPlatformZ);
            return playerPosition == _hackerPosition;
        }

        public void CallOnHackerSpawn(Vector2Int startPosition, WorldGenerator world, PlayerMovement playerMovement)
        {
            TargetPlayer = playerMovement;
            World = world;
            
            transform.position = World[startPosition.x, startPosition.y].PlayerPivot.position;
            _hackerPosition = startPosition;
            ActionTimer = defaultActionTimer;
            _currentActionTimer = ActionTimer;
        }
    }
}