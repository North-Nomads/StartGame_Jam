using Player;
using System.IO;
using Level;
using Camera;
using UnityEngine;
using Utils;
using UI;
using Cinemachine;

namespace WorldGeneration
{
    /// <summary>
    /// A start point of the level. Responsible for initialization 
    /// </summary>
    public class WorldGenerator : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private CameraShake cameraShake;
        
        
        [SerializeField] private EndGameMenu endGameMenu;
        [SerializeField] private int FinishID;

        // Prefabs of platforms (index = id)
        [SerializeField] private SerializableDictionary<int, WorldPlatform> platformPrefabs;
        // Effects array that are sorted by their id
        [SerializeField] private SerializableDictionary<int, PlatformEffect> platformEffects;
        // Player prefab
        [SerializeField] private PlayerMovement playerPrefab;

        // Array of platforms 
        private WorldPlatform[,] _worldPlatforms;
        private Vector2Int _finishPosition;
        private PlayerMovement _player;
        
        // Size of 2d array
        private int _levelSizeX;
        private int _levelSizeZ;

        public Vector2Int FinishPosition => _finishPosition;

        public WorldPlatform this[int x, int z] => _worldPlatforms[x, z];

        /// <summary>
        /// Gets the X-size of the level.
        /// </summary>
        public int LevelSizeX => _levelSizeX;

        /// <summary>
        /// Gets the Z-size of the level.
        /// </summary>
        public int LevelSizeZ => _levelSizeZ;

        public CameraShake CameraShake => cameraShake;

        private void Start()
        {
            ReloadLevel();
        }

        public void ReloadLevel()
        {
            if (_player != null)
            {
                Destroy(_player.gameObject);
            }
            for (int i = 0; i < LevelSizeX; i++)
            {
                for (int j = 0; j < LevelSizeZ; j++)
                {
                    var platform = _worldPlatforms[i, j];
                    Destroy(platform.gameObject);
                    if (platform.Effect != null)
                    {
                        Destroy(platform.Effect.gameObject);
                    }
                }
            }

            // Read the file
            LoadLevel($"Level{SceneIDs.LoadedLevelID}");

            // Spawn the player
            _player = Instantiate(playerPrefab);
            _player.HandleOnInstantiation(this);

            virtualCamera.Follow = _player.ChildTransform;

            LevelJudge.WinLoseScreen = endGameMenu;
            PauseMenu.SetPlayerControls(true);
        }

        /// <summary>
        /// Loads the level from its binary file.
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadLevel(string filePath)
        {
            TextAsset asset = Resources.Load<TextAsset>(Path.Combine("Levels", filePath));
            using Stream stream = new MemoryStream(asset.bytes);
            using BinaryReader reader = new(stream);
            _levelSizeZ = reader.ReadByte();
            _levelSizeX = reader.ReadByte();
            int version = reader.ReadInt32();
            Debug.Log($"Opened world saved in editor version key: {version}");
            _worldPlatforms = new WorldPlatform[_levelSizeX, _levelSizeZ];
            for (int j = 0; j < _levelSizeZ; j++) 
            {
                for (int i = 0; i < _levelSizeX; i++)
                {
                    byte id = reader.ReadByte();
                    if (id == FinishID)
                    {
                        _finishPosition = new Vector2Int(i, j);
                    }
                    byte effectId = reader.ReadByte();
                    PlatformFlags flags = (PlatformFlags)reader.ReadByte();
                    int rotation = 0;
                    if ((flags & PlatformFlags.RotateBy90) != 0)
                        rotation += 90;
                    if ((flags & PlatformFlags.RotateBy180) != 0)
                        rotation += 180;
                    var tile = Instantiate(platformPrefabs[id]);
                    var effectPrefab = platformEffects[effectId];
                    if (effectPrefab != null)
                    {
                        var effect = Instantiate(effectPrefab);
                        effect.transform.position = new Vector3(i, 0, j);
                        tile.Effect = effect;
                    }
                    tile.transform.Rotate(0, rotation, 0);
                    tile.transform.position = new Vector3(i, 0, j);
                    tile.X = i;
                    tile.Z = j;
                    _worldPlatforms[i, j] = tile;
                }
            }
        }
    }
}
