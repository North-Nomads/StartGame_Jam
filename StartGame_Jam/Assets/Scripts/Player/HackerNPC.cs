using System.Collections;
using Level;
using UnityEngine;
using WorldGeneration;

namespace Player
{
    public class HackerNPC : MonoBehaviour
    {
        [SerializeField] private float defaultActionTimer;
        [SerializeField] private Animator animator;

        private float _currentActionTimer;
        private Vector2Int _hackerPosition;
        private float _startDelay;
        private bool _hasKilledPlayer;

        public float DefaultActionTimer => defaultActionTimer;
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
            if (_hasKilledPlayer)
                return;
            
            animator.SetTrigger("Jump");
            animator.SetFloat("JumpSpeed", 1 / ActionTimer);

            var possibleTarget = TargetPlayer.PlayerPath.Peek();
            var xDistance = Mathf.Abs(TargetPlayer.PlayerPlatformX - possibleTarget.x);
            var zDistance = Mathf.Abs(TargetPlayer.PlayerPlatformZ - possibleTarget.y);

            var barrierRadius = TargetPlayer.BarrierRadius;
            
            // If target platform is in the barrier radius 
            if (xDistance <= barrierRadius && zDistance <= barrierRadius && TargetPlayer.HasBarrier)
                return;
            
            var targetPlatform = TargetPlayer.PlayerPath.Dequeue();
            transform.position = World[targetPlatform.x, targetPlatform.y].PlayerPivot.position;
            _hackerPosition = targetPlatform;

            if (HasReachedPlayer())
            {
                _hasKilledPlayer = true;
                LevelJudge.PauseMenu.TakeAwayPlayerControls();
                animator.SetTrigger("Attack");

                StartCoroutine(WaitForKillAnimation());
                
                IEnumerator WaitForKillAnimation()
                {
                    yield return new WaitForSeconds(1f);
                    LevelJudge.WinLoseScreen.ShowLoseMenu();
                }
            }
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