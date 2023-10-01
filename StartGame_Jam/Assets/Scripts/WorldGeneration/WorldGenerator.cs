using Player;
using System.IO;
using Level;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace WorldGeneration
{
    /// <summary>
    /// A start point of the level. Responsible for initialization 
    /// </summary>
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField] private EndGameMenu endGameMenu;

        // Prefabs of platforms (index = id)
        [SerializeField] private SerializableDictionary<int, WorldPlatform> platformPrefabs;
        // Effects array that are sorted by their id
        [SerializeField] private SerializableDictionary<int, PlatformEffect> platformEffects;
        // Player prefab
        [SerializeField] private PlayerMovement playerPrefab;
        
        // Size of 2d array
        private int _levelSizeX;
        private int _levelSizeZ;

        // Array of platforms 
        private WorldPlatform[,] _worldPlatforms;
        private Vector2Int _finishPosition;

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

        private void Start()
        {
            // Read txt
            LoadLevel(SceneManager.GetActiveScene().name + ".map");
            // Initialize level
            
            // Spawn player
            var player = Instantiate(playerPrefab);
            player.HandleOnInstantiation(this);

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
                    byte effectId = reader.ReadByte();
                    PlatformFlags flags = (PlatformFlags)reader.ReadByte();
                    int rotation = 0;
                    if ((flags & PlatformFlags.RotateBy90) != 0) 
                        rotation += 90;
                    if ((flags & PlatformFlags.RotateBy180) != 0)
                        rotation += 180;
                    var tile = Instantiate(platformPrefabs[id]);
                    var effect = platformEffects[effectId];
                    tile.transform.Rotate(0, rotation, 0);
                    tile.transform.position = new Vector3(i, 0, j);
                    tile.Effect = effect;
                    tile.X = i;
                    tile.Z = j;
                    _worldPlatforms[i, j] = tile;
                }
            }
        }

        public void HandlePlayerLose()
        {
            print("Player lost");
            throw new System.NotImplementedException();
        }
    }
}
