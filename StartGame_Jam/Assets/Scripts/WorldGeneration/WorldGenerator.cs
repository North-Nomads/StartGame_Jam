using Player;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WorldGeneration
{
    /// <summary>
    /// A start point of the level. Responsible for initialization 
    /// </summary>
    public class WorldGenerator : MonoBehaviour
    {
        // Size of 2d array
        [SerializeField] private int levelSizeX;
        [SerializeField] private int levelSizeZ;
        
        // Prefabs of platforms (index = id)
        [SerializeField] private WorldPlatform[] platformPrefabs;
        // Effects array that are sorted by their id
        [SerializeField] private PlatformEffect[] platformEffects;
        // Player prefab
        [SerializeField] private PlayerMovement playerPrefab;
        // HackerNPC prefab

        // Array of platforms 
        private WorldPlatform[,] _worldPlatforms;

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
            // Read txt
            LoadLevel(SceneManager.GetActiveScene().name + ".map");
            // Initialize level
            
            // Spawn player
            var player = Instantiate(playerPrefab);
            player.World = this;
            
            int playerStartX = 0;
            int playerStartZ = _worldPlatforms.GetLength(1) / 2;
            
            player.PlayerPlatformX = playerStartX;
            player.PlayerPlatformZ = playerStartZ;
            player.transform.position = _worldPlatforms[playerStartX, playerStartZ].PlayerPivot.position;
        }

        /// <summary>
        /// Loads the level from its binary file.
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadLevel(string filePath)
        {
            using Stream stream = File.OpenRead(Path.Combine(Application.dataPath, "Resources", "Levels", filePath));
            using BinaryReader reader = new(stream);
            levelSizeX = reader.ReadByte();
            levelSizeZ = reader.ReadByte();
            _worldPlatforms = new WorldPlatform[levelSizeX, levelSizeZ];
            for (int i = 0; i < levelSizeX; i++)
            {
                for (int j = 0; j < levelSizeZ; j++)
                {
                    byte id = reader.ReadByte();
                    var tile = Instantiate(platformPrefabs[id]);
                    tile.transform.position = new Vector3(i, 0, j);
                    tile.X = i;
                    tile.Z = j;
                    _worldPlatforms[i, j] = tile;
                }
            }
            for (int i = 0; i < levelSizeX; i++)
            {
                for (int j = 0; j < levelSizeZ; j++)
                {
                    byte id = reader.ReadByte();
                    if (id != 0)
                    {
                        var effect = platformEffects[id];
                        _worldPlatforms[i, j].Effect = effect;
                    }
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
