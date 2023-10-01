using Player;
using System.IO;
using Level;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField] private EndGameMenu endGameMenu;

        [SerializeField] private int FinishID;

        // Size of 2d array
        [SerializeField] private int levelSizeX;
        [SerializeField] private int levelSizeZ;

        // Prefabs of platforms (index = id)
        [SerializeField] private SerializableDictionary<int, WorldPlatform> platformPrefabs;
        // Effects array that are sorted by their id
        [SerializeField] private SerializableDictionary<int, PlatformEffect> platformEffects;
        // Player prefab
        [SerializeField] private PlayerMovement playerPrefab;
        [SerializeField] private CameraMovement cameraMovement;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        // Array of platforms 
        private WorldPlatform[,] _worldPlatforms;
        private Vector2Int _finishPosition;
        private PlayerMovement _player;

        public Vector2Int FinishPosition => _finishPosition;

        public WorldPlatform this[int x, int z] => _worldPlatforms[x, z];

        /// <summary>
        /// Gets the X-size of the level.
        /// </summary>
        public int LevelSizeX => levelSizeX;

        /// <summary>
        /// Gets the Z-size of the level.
        /// </summary>
        public int LevelSizeZ => levelSizeZ;

        private void Start()
        {
            ReloadLevel();
        }

        public void ReloadLevel()
        {
            if (_player != null)
            {
                Destroy(_player);
            }
            for (int i = 0; i < LevelSizeX; i++)
            {
                for (int j = 0; j < LevelSizeZ; j++)
                {
                    Destroy(_worldPlatforms[i, j]);
                }
            }

            // Read the file
            LoadLevel($"Level{SceneIDs.LoadedLevelID}.map");

            // Spawn the player
            _player = Instantiate(playerPrefab);
            _player.cinemachineVirtualCamera = virtualCamera;
            cameraMovement.ObjToFollow = _player.transform;
            virtualCamera.Follow = _player.transform;
            virtualCamera.GetComponent<CameraShake>()._cinemachineVirtualCamera = virtualCamera;
            _player.HandleOnInstantiation(this);

            LevelJudge.WinLoseScreen = endGameMenu;
        }

        /// <summary>
        /// Loads the level from its binary file.
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadLevel(string filePath)
        {
            using Stream stream = File.OpenRead(Path.Combine(Application.dataPath, "Resources", "Levels", filePath));
            using BinaryReader reader = new(stream);
            levelSizeZ = reader.ReadByte();
            levelSizeX = reader.ReadByte();
            int version = reader.ReadInt32();
            Debug.Log($"Opened world saved in editor version key: {version}");
            _worldPlatforms = new WorldPlatform[levelSizeX, levelSizeZ];
            for (int j = 0; j < levelSizeZ; j++) 
            {
                for (int i = 0; i < levelSizeX; i++)
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
