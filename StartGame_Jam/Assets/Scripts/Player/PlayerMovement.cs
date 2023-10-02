using System.Collections;
using Level;
﻿using Cinemachine;

using System.Collections.Generic;
using Camera;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using WorldGeneration;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float defaultPlayerActionTime;
        [SerializeField] private float hackerDelay;
        [SerializeField] private HackerNPC hacker;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform childObject;

        private float _currentActionCooldown; // Movement timer (updating in Update())
        private readonly List<Vector2Int> _playerPath = new();
        private Vector2 _moveInput;
        private EndGameMenu _endGameMenu;
        private bool CanMoveNow => _currentActionCooldown <= 0 && !PauseMenu.IsPaused;

        public CameraShake CameraShake { get; set; }
        
        public int BarrierRadius { get; set; }
        
        
        public bool HasShield { get; set; }
        
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
        public List<Vector2Int> PlayerPath => _playerPath;
        
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
        /// Are inputs reversed by each axis separately? Called by DDOSEffect
        /// </summary>
        public bool AreInputsReversed { get; set; }

        public bool HasBarrier => BarrierRadius > 0;
        public Transform ChildTransform => childObject;

        private void Update()
        {
            if (_currentActionCooldown > 0)
                _currentActionCooldown -= Time.deltaTime;
        }

        /// <summary>
        /// Checks if player can move in certain direction (south, west, north or east)
        /// Called from InputSystem
        /// </summary>
        public void OnTryMovingSelfOnPlatform(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                // Get player input
                if (!CanMoveNow)
                    return;
                
                _moveInput = context.ReadValue<Vector2>();

                int moveX = DefineWorldSide(_moveInput.x);
                int moveZ = DefineWorldSide(_moveInput.y);

                if (moveX != 0 && moveZ != 0)
                    return;

                int targetX = PlayerPlatformX + moveX;
                int targetZ = PlayerPlatformZ + moveZ;
                
                if (targetX < 0 || targetX >= World.LevelSizeX || targetZ < 0 || targetZ >= World.LevelSizeZ)
                {
                    CameraShake.ShakeCameraOnBump();
                    return;
                }
                
                // Calculate target platform position
                var targetPlatform = World[targetX, targetZ];

                // Check if target platform is available to be stand on
                // Call MoveSelfOnPlatform(x, z) where x, z are indices of 2d array for target platform 
                if (targetPlatform.IsReachable && PauseMenu.IsCharacterControllable && !PauseMenu.IsPaused)
                {
                    if (targetPlatform.Effect != null && targetPlatform.Effect.IsPickable)
                    {
                        targetPlatform.Effect.ExecuteOnPickUp(this);
                        targetPlatform.DisableEffect();
                    }
                    MoveSelfOnPlatform(targetX, targetZ);
                    _currentActionCooldown = ActionCooldown;
                }
                else
                {
                    CameraShake.ShakeCameraOnBump();
                }
            }
            
            int DefineWorldSide(float input)
            {
                var value = 0;
                if (input == 0)
                    return value;
                
                if (input > 0)
                    value = -1;
                else 
                    value = 1;

                if (AreInputsReversed)
                    value *= -1;
                return value;
            }
        }
        
        /// <summary>
        /// Moves self on x, z and checks if player loses
        /// </summary>
        /// <param name="x">target platform x coordinate</param>
        /// <param name="z">target platform z coordinate</param>
        private void MoveSelfOnPlatform(int x, int z)
        {
            Vector2Int currentPosition = new(PlayerPlatformX, PlayerPlatformZ);
            transform.rotation = OrganicMovements.ConvertInputIntoRotation(PlayerPlatformX - x, PlayerPlatformZ - z);
            
            animator.SetTrigger("Jump");
            animator.SetFloat("JumpSpeed", 1 / ActionCooldown);
            _playerPath.Add(new Vector2Int(x, z));

            StartCoroutine(PerformMovingTowardsTarget());
            PlayerPlatformX = x;
            PlayerPlatformZ = z;

            if (World[x, z].IsCheckPoint)
            {
                Debug.Log("Checkpoint has been reached");
                _playerPath.Clear();
                Destroy(Hacker.gameObject);
                Hacker = null;
                return;
            }

            if (x == World.FinishPosition.x && z == World.FinishPosition.y)
                LevelJudge.WinLoseScreen.ShowWinMenu();
            
            // Init hacker if this input is first one
            if (Hacker is not null)
            {
                if (Hacker.HasReachedPlayer())
                    Hacker.KillPlayer();
                return;
            }

            Hacker = Instantiate(hacker);
            Hacker.CallOnHackerSpawn(World, this, hackerDelay, currentPosition);

            IEnumerator PerformMovingTowardsTarget()
            {
                var target = World[x, z].PlayerPivot.position;
                var distance = target - transform.position;

                var delta = distance / ActionCooldown;
                
                var time = 0f;

                while (time < ActionCooldown)
                {
                    transform.position += delta * Time.deltaTime;
                    time += Time.deltaTime;
                    yield return null;                
                }
            }
        }

        public void ReturnOneStepBack()
        {
            var targetPosition = _playerPath[^1];
            _playerPath.RemoveAt(_playerPath.Count - 1);
            print($"Returning back 1 step to {targetPosition}");
            MoveSelfOnPlatform(targetPosition.x, targetPosition.y);
        }

        public void HandleBarrierBlock()
        {
            // handle VFX
            HasShield = false;
        }

        public void HandleOnInstantiation(WorldGenerator world)
        {
            ActionCooldown = defaultPlayerActionTime;
            World = world;
            CameraShake = World.CameraShake;
            
            PlayerPlatformX = 0;
            PlayerPlatformZ = 0;
            transform.position = World[0, 0].PlayerPivot.position;
        }

        public Vector2Int SeeNextStep() => PlayerPath[0];

        public Vector2Int GetNextStep()
        {
            var request = PlayerPath[0];
            PlayerPath.RemoveAt(0);
            return request;
        }
    }
}