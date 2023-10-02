using System.Collections;
using Level;
using UI;
using UnityEngine;
using Utils;
using WorldGeneration;

namespace Player
{
    public class HackerNPC : MonoBehaviour
    {
        [SerializeField] private float defaultActionTimer;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip sound;

        private float _currentActionTimer;
        private Vector2Int _hackerPosition;
        private float _startDelay;
        private bool _hasKilledPlayer;

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

            var possibleTarget = TargetPlayer.SeeNextStep();
            transform.rotation = OrganicMovements.ConvertInputIntoRotation(_hackerPosition.x - possibleTarget.x,
                _hackerPosition.y - possibleTarget.y);
            var xDistance = Mathf.Abs(TargetPlayer.PlayerPlatformX - possibleTarget.x);
            var zDistance = Mathf.Abs(TargetPlayer.PlayerPlatformZ - possibleTarget.y);

            var barrierRadius = TargetPlayer.BarrierRadius;
            
            // If target platform is in the barrier radius 
            if (xDistance <= barrierRadius && zDistance <= barrierRadius && TargetPlayer.HasBarrier)
                return;
            
            var targetPlatform = TargetPlayer.GetNextStep();
            transform.position = World[targetPlatform.x, targetPlatform.y].PlayerPivot.position;
            _hackerPosition = targetPlatform;

            if (HasReachedPlayer())
                KillPlayer();
        }

        public void KillPlayer()
        {
            if (SoundManager.IsSoundEnabled)
                source.PlayOneShot(sound);
            _hasKilledPlayer = true;
            
            PauseMenu.SetPlayerControls(false);
            animator.SetTrigger("Attack");

            StartCoroutine(WaitForKillAnimation());
                
            IEnumerator WaitForKillAnimation()
            {
                yield return new WaitForSeconds(1f);
                PauseMenu.SetPlayerControls(true);
                LevelJudge.WinLoseScreen.ShowLoseMenu();
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

        public void CallOnHackerSpawn(WorldGenerator world, PlayerMovement playerMovement, float hackerDelay, Vector2Int checkpointPosition)
        {
            _hasKilledPlayer = false;
            TargetPlayer = playerMovement;
            World = world;
            
            transform.position = World[checkpointPosition.x, checkpointPosition.y].PlayerPivot.position;
            _hackerPosition = Vector2Int.zero;
            ActionTimer = defaultActionTimer;
            _currentActionTimer = ActionTimer;
            _startDelay = hackerDelay;
        }
    }
}